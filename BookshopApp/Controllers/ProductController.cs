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

        [HttpGet("ProductManipulator/{id:int}")]
        public async Task<IActionResult> GetProductAndAuthors(int id)
        {
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetFullProduct(id));
            
            return Ok(new { product = productOut, authors = "data"});
        }

            [HttpPost("ProductManipulator/{id:int}")]
        public async Task<IActionResult> ProductManipulator(int id, [FromForm] ProductInputDto uploadedData)
        {
            //костыль. Сделал по 2-м причинам:
            //1 - нужно было по тз заюзать хоть раз отправку форм средствами html (т.е. через form)
            //2 - проблемы с временем
            //Если id=0, значит необходимо создавать новый объект
            if(id == 0)
            {
                var product = _mapper.Map<Product>(uploadedData);
                await SaveImageAndAssignField(product);
                await _unitOfWork.ProductsRepository.AddEntityAsync(product);
            }
            else
            {
                var productFromDb = await _unitOfWork.ProductsRepository.GetEntityAsync(id);
                productFromDb = _mapper.Map(uploadedData, productFromDb);
                await SaveImageAndAssignField(productFromDb);
            }

            if (await _unitOfWork.Commit())
                return Redirect("/");
            else
                return BadRequest();


            async Task SaveImageAndAssignField(Product prod)
            {
                if (uploadedData.ImageFile != null)
                {
                    var linkToImage = Path.Combine("Images", uploadedData.ImageFile.FileName);
                    var path = Path.Combine(_environment.ContentRootPath, "ClientApp", "public", linkToImage);

                    using var fileStream = new FileStream(path, FileMode.Create);
                    await uploadedData.ImageFile.CopyToAsync(fileStream);

                    prod.LinkToImage = linkToImage;
                }
            }
        }
    }
}
