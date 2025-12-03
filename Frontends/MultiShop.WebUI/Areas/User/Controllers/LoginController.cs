using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.IdentityDtos.LoginDto;
using MultiShop.WebUI.Services.Abstract;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
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
                bool result = await _identityService.SignIn(createLoginDto, "Member");

                if (result)
                {
                    return RedirectToAction("Index", "Profile", new { area = "User" });
                }
                else
                {
                    ModelState.AddModelError("", "Giriş başarısız. Kullanıcı adı/şifre hatalı veya yetkiniz yok.");
                }
            }
            return View();
        }
    }
}
