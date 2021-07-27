using AutoMapper;
using BookshopApp.Db;
using BookshopApp.Models;
using BookshopApp.Models.DTO;
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

        [HttpGet("prods/{page:int}")]
        public IActionResult GetProducts(int page)
        {
            var requestData = _unitOfWork.ProductsRepository.GetProductsAsync(page, CountOfProductsOnPage);

            return Ok(requestData);
        }

        [HttpGet("product/{id:int}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var productOut = _mapper.Map<ProductDto>(await _unitOfWork.ProductsRepository.GetEntityAsync(id));
            return Ok(productOut);
        }

        [Authorize]
        [HttpPost("buy")]
        public async Task<IActionResult> AddProductToBasket(int productId, int count)
        {
            if (count < 1)
                return BadRequest();

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var basket = await _unitOfWork.OrdersRepository.GetUserBasketAsync(user.Id);

            if (basket is null)
            {
                basket = new Order() {CustomerId = user.Id, StateId = (int)OrderStateEnum.IsBasket };

                await _unitOfWork.OrdersRepository.AddEntityAsync(basket);
            }

            //если корзина существует, но пуста. Например, добавил и удалил продукт. Сработает условие?
            if (basket.OrderedProducts?.Find(o => o.ProductId == productId) == null)
            {
                if(basket.OrderedProducts == null)
                    basket.OrderedProducts = new List<OrderedProduct>();

                basket.OrderedProducts.Add(new OrderedProduct() { ProductId = productId, Count = count, OrderId = basket.Id, TimeOfBuing = DateTime.Now });
            }

            if(await _unitOfWork.Commit())
                return Ok();
            else
                return BadRequest();
        }
    }
}
