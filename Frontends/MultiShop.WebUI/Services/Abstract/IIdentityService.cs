using MultiShop.DtoLayer.IdentityDtos.LoginDto;

namespace MultiShop.WebUI.Services.Abstract
{
    public interface IIdentityService
    {
        Task<bool> SignIn(CreateLoginDto createLoginDto, string requiredRole = null);
        Task<bool> GetRefreshToken();
    }
}
