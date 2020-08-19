using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.WebApp.Handlers;
using AnagramSolver.WebApp.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
                    .AddScoped<IWordRepositoryEF, Data.EntityFramework.WordRepository>()
                    .AddScoped<IWordService, BusinessLogic.Services.WordService>()
                    .AddScoped<IRestrictionService, BusinessLogic.Services.RestrictionService>()
                    .AddScoped<ICachedWordService, BusinessLogic.Services.CachedWordService>()
                    .AddScoped<ICachedWordRepository, Data.EntityFramework.CachedWordRepositoryEF>()
                    .AddScoped<ILogService, BusinessLogic.Services.LogService>()
                    .AddScoped<IUserLogRepository, Data.EntityFramework.UserLogRepositoryEF>()
                    .AddHttpContextAccessor();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

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
            app.UseMiddleware<ContextHandler>();
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
