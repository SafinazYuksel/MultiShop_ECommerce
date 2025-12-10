using MultiShop.WebUI.Services.Abstract;
using System.Security.Claims;

namespace MultiShop.WebUI.Services.Concrete
{
    public class LoginService : ILoginService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor contextAccessor)
        {
            _httpContextAccessor = contextAccessor;
        }

        public string GetUserId
        {
            get
            {
                // 1. Accessor null mı?
                if (_httpContextAccessor == null)
                {
                    return "Accessor Null";
                }

                // 2. HttpContext null mı? (Hatanın sebebi muhtemelen burası)
                if (_httpContextAccessor.HttpContext == null)
                {
                    return "HttpContext Null - Program.cs'de AddHttpContextAccessor ekli mi?";
                }

                // 3. User (Identity) null mı?
                if (_httpContextAccessor.HttpContext.User == null)
                {
                    return "User Null - Token geçersiz veya Authentication middleware çalışmadı";
                }

                // 4. İstenen Claim var mı?
                var claim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

                // Eğer NameIdentifier ile bulamıyorsa "sub" dene
                if (claim == null)
                {
                    claim = _httpContextAccessor.HttpContext.User.FindFirst("sub");
                }

                return claim?.Value ?? "User ID Bulunamadı";
            }
        }
    }
}
