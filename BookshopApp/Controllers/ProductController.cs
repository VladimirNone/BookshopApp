using AutoMapper;
using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using BookshopApp.Models.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        private int CountOfProductsOnPage = 10;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet("Prods/{page:int}")]
        public async Task<IActionResult> GetProducts(int page)
        {
            (var prods, var pageIsLast) = await _unitOfWork.ProductsRepository.GetProductsAsync(page, CountOfProductsOnPage);

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
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetFullProductAsync(id));
            return Ok(productOut);
        }
    }
}
