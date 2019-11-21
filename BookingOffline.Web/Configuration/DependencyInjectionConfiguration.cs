using BookingOffline.Common;
using BookingOffline.Repositories;
using BookingOffline.Repositories.Interfaces;
using BookingOffline.Services;
using BookingOffline.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookingOffline.Web.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddBSSevices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
        }

        public static void AddDASevices(this IServiceCollection services)
        {
            services.AddScoped<IAlipayUserRepository, AlipayUserRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderItemRepository, OrderItemRepository>(); 
            services.AddScoped<SQLiteDBContext>();
        }

        public static void AddCommonSevices(this IServiceCollection services)
        {
            services.AddScoped<IAlipayService, AlipayService>();
            services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        }
    }
}
