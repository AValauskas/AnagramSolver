using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.WebApp.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SoapCore;
using System.ServiceModel;

namespace AnagramSolver.WebService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IAnagramSolver, BusinessLogic.AnagramSolver>()
                      .AddScoped<IWordRepositoryEF, Data.EntityFramework.WordRepository>()
                      .AddScoped<IWordService, BusinessLogic.Services.WordService>()             
                      .AddScoped<IAnagramService, AnagramService>()
                      .AddHttpContextAccessor();
           

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            
            services.AddMvc();

            services.AddControllersWithViews();
            services.AddDbContext<AnagramSolverDBContext>(opt =>
            opt.UseSqlServer(Settings.ConnectionString));

           services.TryAddSingleton<IAnagramService, AnagramService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSoapEndpoint<IAnagramService>(path:"/AnagramService.svc", binding: new BasicHttpBinding());

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
