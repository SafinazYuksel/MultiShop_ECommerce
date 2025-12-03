using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using MultiShop.IdentityServer.Tools;
using System.Threading.Tasks;

namespace MultiShop.IdentityServer.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginsController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user == null)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Contains("Admin"))
                {
                    return StatusCode(403, "Bu panele erişim yetkiniz yok! (Sadece Adminler girebilir)");
                }

                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel
                {
                    Username = userLoginDto.Username,
                    Id = user.Id,
                    Role = "Admin"
                };

                var token = JwtTokenGenerator.GenerateToken(model);
                return Ok(token);
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı!");
            }
        }

        [HttpPost("UserLogin")]
        public async Task<IActionResult> UserLogin(UserLoginDto userLoginDto)
        {
            var user = await _userManager.FindByNameAsync(userLoginDto.Username);

            if (user == null)
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDto.Password, false);

            if (result.Succeeded)
            {
                var roles = await _userManager.GetRolesAsync(user);

                if (!roles.Contains("Admin"))
                {
                    return StatusCode(403, "Bu panele erişim yetkiniz yok! (Sadece Adminler girebilir)");
                }

                GetCheckAppUserViewModel model = new GetCheckAppUserViewModel
                {
                    Username = userLoginDto.Username,
                    Id = user.Id,
                    Role = "Admin"
                };

                var token = JwtTokenGenerator.GenerateToken(model);
                return Ok(token);
            }
            else
            {
                return BadRequest("Kullanıcı adı veya şifre hatalı!");
            }
        }
    }
}
