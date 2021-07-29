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
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetEntityAsync(id));
            return Ok(productOut);
        }

        [Authorize]
        [HttpPost("Buy")]
        public async Task<IActionResult> AddProductToBasket(BuyDto buy)
        {
            if (buy.Count < 1)
                return BadRequest();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _unitOfWork.OrdersRepository.GetUserBasketAsync(user.Id);

            if (cart is null)
            {
                cart = new Order() {CustomerId = user.Id, StateId = (int)OrderStateEnum.IsCart };

                await _unitOfWork.OrdersRepository.AddEntityAsync(cart);
            }

            //if the cart exists, but is empty. For example, add and deltet product. This condition will work?
            if (cart.OrderedProducts?.Find(o => o.ProductId == buy.ProductId) == null)
            {
                if(cart.OrderedProducts == null)
                    cart.OrderedProducts = new List<OrderedProduct>();

                cart.OrderedProducts.Add(new OrderedProduct() { ProductId = buy.ProductId, Count = buy.Count, OrderId = cart.Id, TimeOfBuing = DateTime.Now });
            }

            if(await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
