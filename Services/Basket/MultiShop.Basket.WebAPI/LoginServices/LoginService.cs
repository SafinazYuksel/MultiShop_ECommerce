namespace MultiShop.Basket.WebAPI.LoginServices
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
                return _httpContextAccessor.HttpContext.User.FindFirst("sub")?.Value;
            }
        }
    }
}
