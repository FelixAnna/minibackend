using BookingOffline.Common;
using BookingOffline.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BookingOffline.Web.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddBSSevices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
        }

        public static void AddDASevices(this IServiceCollection services)
        {
        }

        public static void AddCommonSevices(this IServiceCollection services)
        {
            services.AddScoped<AlipayService>();
            services.AddScoped<TokenGeneratorService>();
        }
    }
}
