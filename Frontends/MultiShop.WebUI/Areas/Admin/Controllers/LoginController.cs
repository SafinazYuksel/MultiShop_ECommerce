using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDto;
using MultiShop.WebUI.Services.Abstract;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly IIdentityService _identityService;

        public LoginController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult SingIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SingIn(CreateLoginDto createLoginDto)
        {
            if (ModelState.IsValid)
            {
                bool result = await _identityService.SignIn(createLoginDto, "Admin");

                if (result)
                {
                    return RedirectToAction("Index", "Statistic", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("", "Giriş başarısız. Kullanıcı adı/şifre hatalı veya yetkiniz yok.");
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("SingIn", "Login", new { area = "Admin" });
        }
    }
}
