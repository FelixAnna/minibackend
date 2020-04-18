using BookingOffline.Common;
using BookingOffline.Repositories.SqlServer; //or Sqlite
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using BookingOffline.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingOffline.Web.Configurations
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddBSSevices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<IUserService, UserService>();
        }

        public static void AddDASevices(this IServiceCollection services)
        {
            services.AddScoped<IAlipayUserRepository, AlipayUserRepository>();
            services.AddScoped<IWechatUserRepository, WechatUserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            services.AddScoped<BODBContext>();
        }

        public static void AddCommonSevices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAlipayService, AlipayService>(provider => new AlipayService(configuration["Alipay:AppId"], configuration["Alipay:PrivateKey"], configuration["Alipay:PublicKey"]));
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();

            services.AddScoped<IWechatService, WechatService>(provider => new WechatService(configuration["Wechat:AppId"], configuration["Wechat:Secret"]));
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        }
    }
}
