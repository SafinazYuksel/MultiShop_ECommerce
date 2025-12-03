using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.RegisterDto;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class RegisterController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegisterController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingUp(CreateRegisterDto createRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(createRegisterDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PostAsync("https://localhost:5001/api/Registers/UserRegister", stringContent);
                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("SingIn", "Login", new { area = "User" });
                }
            }
            return View();
        }
    }
}
