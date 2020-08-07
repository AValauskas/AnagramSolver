using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.EF.CodeFirst;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
                    .AddScoped<IWordRepository, Data.EntityFramework.WordRepository>()
                    //.AddScoped<IWordRepository, DatabaseWordRepository>()
                    // .AddScoped<IWordRepository, WordRepository>()
                    .AddScoped<IWordService, BusinessLogic.Services.WordService>()
                    .AddScoped<ICachedWordService, BusinessLogic.Services.CachedWordService>()
                    //.AddScoped<ICachedWordRepository, Data.Database.CachedWordRepository>()
                    .AddScoped<ICachedWordRepository, Data.EntityFramework.CachedWordRepositoryEF>()
                    .AddScoped<ILogService, BusinessLogic.Services.LogService>()
                    //.AddScoped<IUserLogRepository, Data.Database.UserLogRepository>()
                    .AddScoped<IUserLogRepository, Data.EntityFramework.UserLogRepositoryEF>()
                    .AddHttpContextAccessor();

            services.AddControllersWithViews();
            services.AddDbContext<AnagramSolverDBContext>(opt =>
            opt.UseSqlServer(Settings.ConnectionString));
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
