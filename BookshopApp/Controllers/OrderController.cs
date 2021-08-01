using AutoMapper;
using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private int CountOfProductsOnPage = 10;

        public OrderController(IUnitOfWork unitOfWork, UserManager<User> userManager, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("Cart/{page:int}")]
        public async Task<IActionResult> GetCart(int page)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _unitOfWork.OrdersRepository.GetUserCartAsync(user.Id, page, CountOfProductsOnPage);
            
            var pageIsLast = false;
            if (cart.OrderedProducts.Count <= CountOfProductsOnPage)
                pageIsLast = true;
            else
                cart.OrderedProducts.Remove(cart.OrderedProducts.Last());

            //Cart page don't provide description for product
            foreach (var item in cart.OrderedProducts)
                item.Product.Description = null;

            //we return CountOfProductsOnPage items, but for determining - Is this page the last? - we use this condition 
            //if prods.Count() == (CountOfProductsOnPage + 1) then exist next page
            var cartDto = _mapper.Map<CartDto>(cart);

            return Ok(new { cart = cartDto, pageIsLast });
        }

        [Authorize]
        [HttpPost("Cart/Cancel/{id:int}")]
        public async Task<IActionResult> CancelCartedProduct(int id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _unitOfWork.OrdersRepository.GetUserCartAsync(user.Id);

            cart.OrderedProducts.Find(h => h.Id == id).Cancelled = true;

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPost("Cart/PlaceOrder")]
        public async Task<IActionResult> PlaceOrder()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var cart = await _unitOfWork.OrdersRepository.GetUserCartAsync(user.Id);

            cart.StateId = (int)OrderStateEnum.Confirmed;
            cart.DateOfOrdering = DateTime.Now;

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
