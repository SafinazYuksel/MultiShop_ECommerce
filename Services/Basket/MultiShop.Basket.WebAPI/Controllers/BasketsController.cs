using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MultiShop.Basket.WebAPI.Dtos;
using MultiShop.Basket.WebAPI.LoginServices;
using MultiShop.Basket.WebAPI.Services;

namespace MultiShop.Basket.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ILoginService _loginService;

        public BasketsController(IBasketService basketService, ILoginService loginService)
        {
            _basketService = basketService;
            _loginService = loginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasketDetail()
        {
            var userId = _loginService.GetUserId;
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Kullanıcı kimliği doğrulanamadı.");
            }

            var basket = await _basketService.GetBasket(userId);
            if (basket == null)
            {
                var values = await _basketService.GetBasket(_loginService.GetUserId);
                return Ok(values);
            }
            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
        {
            basketTotalDto.UserId = _loginService.GetUserId;
            await _basketService.SaveBasket(basketTotalDto);
            return Ok("Sepetteki değişiklikler kaydedildi.");
        }

        [HttpDelete("DeleteBasket/{userId}")]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            await _basketService.DeleteBasket(userId);
            return Ok("Sepet başarıyla silindi.");
        }
    }
}
