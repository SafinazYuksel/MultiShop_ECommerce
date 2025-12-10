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

            // Eğer Token'dan ID okunamadıysa işlem yapma (veya Anonim id ata)
            if (string.IsNullOrEmpty(userId))
            {
                // Geçici çözüm veya hata mesajı
                return BadRequest("Kullanıcı kimliği doğrulanamadı.");
            }

            var values1 = await _basketService.GetBasket(userId);

            // ... (kodunuzun devamı aynı kalabilir) ...
            if (values1 == null)
            {
                var values = await _basketService.GetBasket(_loginService.GetUserId);
                return Ok(values);
            }
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SaveMyBasket(BasketTotalDto basketTotalDto)
        {
            basketTotalDto.UserId = _loginService.GetUserId;
            await _basketService.SaveBasket(basketTotalDto);
            return Ok("Sepetteki değişiklikler kaydedildi.");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket(string userId)
        {
            await _basketService.DeleteBasket(userId);
            return Ok("Sepet başarıyla silindi.");
        }
    }
}
