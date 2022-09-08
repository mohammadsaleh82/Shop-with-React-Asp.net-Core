using Core.DTOs.BasketDTO;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class BasketController : BaseController
    {
        private readonly IBasketService _basketService;
        private readonly IProduct _productService;
        public BasketController(IBasketService basketService, IProduct productService)
        {
            _basketService = basketService;
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<BasketViewModel>> GetBasket()
        {
            var basket =await _basketService
                .GetBasketByBuyerId(Request.Cookies["BuyerId"]);
            if (basket == null) return NotFound();
            return Ok(basket);
        }

        [HttpPost]
        public async Task<ActionResult> AddItemToBasket(int ProductId, int Quantity)
        {
            var basket =await _basketService.GetBasketByBuyerId(Request.Cookies["BuyerId"]);
            if (basket == null)
            {
                basket = await _basketService.CreateBasket();
                var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
                Response.Cookies.Append("BuyerId", basket.BuyerId,cookieOptions);
            }
            var product =await _productService.GetProductById(ProductId);
            if(product == null) return NotFound();
            await _basketService.AddItem(product, Quantity, basket.Id);
            return CreatedAtAction("GetBasket",basket);
        }

        [HttpDelete]
        public async Task<ActionResult> RemoveItem(int ProductId,int Quantity)
        {
            var basket = await _basketService.GetBasketByBuyerId(Request.Cookies["BuyerId"]);
            if(basket == null)return NotFound();
            await _basketService.Remove(basket.Id,Quantity,ProductId);
            return Ok();
        }
    }
}
