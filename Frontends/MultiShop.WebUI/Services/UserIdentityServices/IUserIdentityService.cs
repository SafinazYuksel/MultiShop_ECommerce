using MultiShop.IdentityServer.Dtos;
using MultiShop.DtoLayer.IdentityDtos.UserDto;

namespace MultiShop.WebUI.Services.UserIdentityServices
{
    public interface IUserIdentityService
    {
        Task<ResultUserDto> GetUserDetailAsync();
        Task<List<ResultUserDto>> GetAllUserListAsync();
        Task UpdateUserDetailAsync(UpdateUserDto updateUserDto);
    }
}
