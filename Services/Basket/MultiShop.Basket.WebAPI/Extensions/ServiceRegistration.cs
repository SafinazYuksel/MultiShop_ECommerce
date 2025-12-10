using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using MultiShop.Basket.WebAPI.LoginServices;
using MultiShop.Basket.WebAPI.Services;
using MultiShop.Basket.WebAPI.Settings;

namespace MultiShop.Basket.WebAPI.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddBasketServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add services to the container.
            services.AddScoped<IBasketService, BasketService>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddHttpContextAccessor();
            services.Configure<RedisSettings>(configuration.GetSection("RedisSettings"));
            services.AddSingleton<RedisService>(sp =>
            {
                var redisSettings = sp.GetRequiredService<IOptions<RedisSettings>>().Value;
                var redis = new RedisService(redisSettings.Host, redisSettings.Port);
                redis.Connect();
                return redis;
            });

            return services;
        }
    }
}
