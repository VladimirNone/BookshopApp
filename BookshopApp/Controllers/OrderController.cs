using AutoMapper;
using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
using BookshopApp.Models.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookshopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private int CountOfProductsOnPage = 10;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [Authorize]
        [HttpGet("Orders/{page:int}")]
        public async Task<IActionResult> GetOrders(int page)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            (var orders, var pageIsLast) = await _unitOfWork.OrdersRepository.GetOrdersNoTracked(userId, page, CountOfProductsOnPage);

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return Ok(new { orders = ordersDto, pageIsLast });
        }

        [Authorize]
        [HttpGet("{id:int}/{page:int}")]
        public async Task<IActionResult> GetOrder(int id, int page)
        {
            (var order, var pageIsLast) = await _unitOfWork.OrdersRepository.GetOrderNoTracked(id, page, CountOfProductsOnPage);

            var ordersDto = _mapper.Map<OrderDto>(order);

            return Ok(new { order = ordersDto, pageIsLast });
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GlobalOrders/{page:int}")]
        public async Task<IActionResult> GetGlobalOrders(int page)
        {
            (var orders, var pageIsLast) = await _unitOfWork.OrdersRepository.GetOrdersNoTracked(page, CountOfProductsOnPage);

            var ordersDto = _mapper.Map<List<OrderDto>>(orders);

            return Ok(new { orders = ordersDto, pageIsLast });
        }

        [Authorize]
        [HttpGet("Cart/{page:int}")]
        public async Task<IActionResult> GetCart(int page)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            (var cart, var pageIsLast) = await _unitOfWork.OrdersRepository.GetOrCreateUserCartNoTracked(userId, page, CountOfProductsOnPage);

            //Cart page don't provide description for product
            foreach (var item in cart.OrderedProducts)
                item.Product.Description = null;

            var cartDto = _mapper.Map<CartDto>(cart);

            var discount = await _unitOfWork.UsersRepository.GetDiscountNoTracked(userId);

            return Ok(new { cart = cartDto, pageIsLast, discountPercent = (discount == null || discount.NumberOfUses == 0) ? 0: discount.Percent });
        }

        [Authorize]
        [HttpPost("Cart/Cancel/{productId:int}")]
        public async Task<IActionResult> CancelCartedProduct(int productId)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _unitOfWork.OrdersRepository.CancelProductCart(userId, productId);

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPost("Cart/PlaceAnOrder")]
        public async Task<IActionResult> PlaceAnOrder()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _unitOfWork.OrdersRepository.PlaceAnOrder(userId);

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }

        [Authorize]
        [HttpPost("AddToCart")]
        public async Task<IActionResult> AddProductToCart(BuyDto buy)
        {
            if (buy.Count < 1)
                return BadRequest();

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _unitOfWork.OrdersRepository.AddToCart(userId, buy.ProductId, buy.Count);

            if (await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
