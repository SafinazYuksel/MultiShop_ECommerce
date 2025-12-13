using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiShop.IdentityServer.Dtos;
using MultiShop.IdentityServer.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace MultiShop.IdentityServer.Controllers
{
    [Authorize(LocalApi.PolicyName)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("GetUserInfo")]
        public async Task<IActionResult> GetUserInfo()
        {
            var userClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            var user = await _userManager.FindByIdAsync(userClaim.Value);
            return Ok(new
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Username = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Country = user.Country,
                City = user.City,
                Website = user.Website,
                Bio = user.Bio
            });
        }

        [HttpGet("GetAllUserList")]
        public async Task<IActionResult> GetAllUserList()
        {
            var users = await _userManager.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(UpdateUserDto updateUserDto)
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userId == null) return BadRequest("Kullanıcı bulunamadı");

            var user = await _userManager.FindByIdAsync(userId.Value);
            if (user == null) return NotFound();

            if (user.Email != updateUserDto.Email)
            {
                var emailResult = await _userManager.SetEmailAsync(user, updateUserDto.Email);
                if (!emailResult.Succeeded) return BadRequest(emailResult.Errors);
            }

            if (user.UserName != updateUserDto.Username)
            {
                var nameResult = await _userManager.SetUserNameAsync(user, updateUserDto.Username);
                if (!nameResult.Succeeded) return BadRequest(nameResult.Errors);
            }

            user.Name = updateUserDto.Name;
            user.Surname = updateUserDto.Surname;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Country = updateUserDto.Country;
            user.City = updateUserDto.City;
            user.Website = updateUserDto.Website;
            user.Bio = updateUserDto.Bio;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded) return Ok("Profil güncellendi");

            return BadRequest(result.Errors);
        }
    }
}
