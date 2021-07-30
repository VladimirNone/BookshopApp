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
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private int CountOfProductsOnPage = 10;

        public ProductController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("Prods/{page:int}")]
        public async Task<IActionResult> GetProducts(int page)
        {
            var prods = await _unitOfWork.ProductsRepository.GetProductsAsync(page, CountOfProductsOnPage);

            var pageIsLast = prods.Count() <= CountOfProductsOnPage;
            //we return CountOfProductsOnPage items, but for determining - Is this page the last? - we use this condition 
            //if prods.Count() == (CountOfProductsOnPage + 1) then exist next page
            var prodsDto = _mapper.Map<ProductDto[]>(prods[..(pageIsLast ? ^0 : CountOfProductsOnPage)]);

            //Description may be very large. Trim it for MainPage
            foreach (var prod in prodsDto)
            {
                prod.Description = prod.Description.Substring(0, 100) + "...";
            }
            return Ok(new { prods = prodsDto, pageIsLast });
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetFullProductAsync(id));
            return Ok(productOut);
        }

        [Authorize]
        [HttpPost("Buy")]
        public async Task<IActionResult> AddProductToCart(BuyDto buy)
        {
            if (buy.Count < 1)
                return BadRequest();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _unitOfWork.OrdersRepository.GetUserCartAsync(user.Id);

            if (cart is null)
            {
                cart = new Order() {CustomerId = user.Id, StateId = (int)OrderStateEnum.IsCart, OrderedProducts = new List<OrderedProduct>() };

                await _unitOfWork.OrdersRepository.AddEntityAsync(cart);
            }

            var orderedProduct = cart.OrderedProducts.Find(h => h.Cancelled == false && h.ProductId == buy.ProductId);
            if (orderedProduct == null)
            {
                cart.OrderedProducts.Add(new OrderedProduct() { ProductId = buy.ProductId, Count = buy.Count, OrderId = cart.Id, TimeOfBuing = DateTime.Now });
                (await _unitOfWork.ProductsRepository.GetEntityAsync(buy.ProductId)).CountInStock -= buy.Count;
            }
            else
            {
                orderedProduct.Count += buy.Count;
                orderedProduct.Product.CountInStock -= buy.Count;
            }

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
