using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Database;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Data.SqlClient;

namespace AnagramSolver.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAnagramSolver, BusinessLogic.AnagramSolver>()
                    //.AddScoped<IWordRepository, DatabaseWordRepository>()
                    .AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IWordService, BusinessLogic.Services.WordService>()
                    .AddHttpContextAccessor();


            services.AddControllersWithViews();

         
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{word?}");
            });
        }
    }
}
