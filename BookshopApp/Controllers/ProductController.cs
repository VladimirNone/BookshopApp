using AutoMapper;
using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using BookshopApp.Models.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        private int CountOfProductsOnPage = 10;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment env)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _environment = env;
        }

        [HttpGet("Prods/{page:int}")]
        public async Task<IActionResult> GetProducts(int page)
        {
            (var prods, var pageIsLast) = await _unitOfWork.ProductsRepository.GetProducts(page, CountOfProductsOnPage);

            var prodsDto = _mapper.Map<List<Product>>(prods);

            //Description may be very large. Trim it for MainPage
            //Need use text-truncate
            foreach (var prod in prodsDto)
                if(prod.Description.Length > 100)
                    prod.Description = prod.Description.Substring(0, 100) + "...";
            
            return Ok(new { prods = prodsDto, pageIsLast });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetFullProduct(id));
            return Ok(productOut);
        }

        [HttpGet("ProductManipulateInfo/{id:int?}")]
        public async Task<IActionResult> GetProductAndAuthors(int? id)
        {
            ProductDto productOut = null;
            if (id != null)
                productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetFullProduct((int)id));

            var authorsOut = _mapper.Map<List<AuthorDto>>(await _unitOfWork.AuthorsRepository.GetAuthors());
            
            return Ok(new { product = productOut, authors = authorsOut });
        }

        [HttpPost("ProductCreate")]
        public async Task<IActionResult> ProductCreate([FromForm] ProductInputDto uploadedData)
        {
            var product = _mapper.Map<Product>(uploadedData);
            if (uploadedData.ImageFile != null)
            {
                product.LinkToImage = await _unitOfWork.ProductsRepository.SaveImage(_environment.ContentRootPath, uploadedData.ImageFile);
            }
            await _unitOfWork.ProductsRepository.AddEntityAsync(product);

            if (await _unitOfWork.Commit())
                //i don't know how do it in front-end part.
                return Redirect("/");
            else
                return BadRequest();
        }

        [HttpPut("ProductChange/{id:int}")]
        public async Task<IActionResult> ProductChange(int id, [FromForm] ProductInputDto uploadedData)
        {
            var productFromDb = await _unitOfWork.ProductsRepository.GetEntityAsync(id);
            productFromDb = _mapper.Map(uploadedData, productFromDb);
            if (uploadedData.ImageFile != null)
            {
                productFromDb.LinkToImage = await _unitOfWork.ProductsRepository.SaveImage(_environment.ContentRootPath, uploadedData.ImageFile);
            }

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
