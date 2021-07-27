using BookshopApp.Db;
using BookshopApp.Models;
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
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;
        private int CountOfProductsOnPage = 10;

        public ProductController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpGet("prods/{page:int}")]
        public IActionResult GetProducts(int page)
        {
            var requestData = _unitOfWork.ProductsRepository.GetProducts(page, CountOfProductsOnPage);

            return Ok(requestData);
        }

        [Authorize]
        [HttpPost("buy")]
        public async Task<IActionResult> AddProductToBasket(int productId, int count)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var basket = await _unitOfWork.OrdersRepository.GetUserBasket(user.Id);

            if (count < 1)
                return BadRequest();

            if (basket == null)
            {
                basket = new Order();
                basket.CustomerId = user.Id;
                basket.StateId = (int)OrderStateEnum.IsBasket;

                await _unitOfWork.OrdersRepository.AddEntityAsync(basket);
            }

            //если корзина существует, но пуста. Например, добавил и удалил продукт. Сработает условие?
            if (basket.OrderedProducts?.Find(o => o.ProductId == productId) == null)
            {
                if(basket.OrderedProducts == null)
                    basket.OrderedProducts = new List<OrderedProduct>();

                basket.OrderedProducts.Add(new OrderedProduct() { ProductId = productId, Count = count, OrderId = basket.Id, TimeOfBuing = DateTime.Now });
            }

            await _unitOfWork.CommitWithException();
            return Ok();
        }
    }
}
