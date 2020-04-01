using BookingOffline.Common;
using BookingOffline.Repositories.SqlServer;
using BookingOffline.Services;
using BookingOffline.Web.Configurations;
using BookingOffline.Web.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingOffline.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddDbContext<BODBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("BODatabase")));

            services.AddSwaggerUI();
            services.AddJwtAutentication(Configuration);

            services.AddBSSevices();
            services.AddDASevices();
            services.AddCommonSevices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<GlobalExceptionHandler>();

            app.UseMiddleware<FakeTokenMiddleware>();
            app.UseSwaggerUI();

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(option => option
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());
            //app.UseMiddleware<CustomMiddleware>();
            app.UseJwtAuthenticaton();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
