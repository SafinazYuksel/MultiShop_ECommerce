using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.WebUI.Services.UserIdentityServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Authorize(Roles = "Member")]
    [Area("User")]
    public class ProfileController : Controller
    {
        private readonly IUserIdentityService _userIdentityService;

        public ProfileController(IUserIdentityService userIdentityService)
        {
            _userIdentityService = userIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Kullanıcı";
            ViewBag.v3 = "Profilim";
            ViewBag.v0 = "Kullanıcı Profili";

            var values = await _userIdentityService.GetUserDetailAsync();
            return View(values);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] UpdateUserDto updateUserDto)
        {
            await _userIdentityService.UpdateUserDetailAsync(updateUserDto);
            return RedirectToAction("Index");
        }
    }
}
