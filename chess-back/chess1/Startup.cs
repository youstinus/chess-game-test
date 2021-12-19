using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using checkers.Configurations;
using checkers.Infrastructure.DataBase;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace chess1
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.Configure<CookiePolicyOptions>(options =>
           {
               // This lambda determines whether user consent for non-essential cookies is needed for a given request.
               options.CheckConsentNeeded = context => true;
               options.MinimumSameSitePolicy = SameSiteMode.None;
           });


           services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
       */
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });
            var mapper = config.CreateMapper();


            services.AddSingleton(mapper);
            services.UseSwagger();
            services.AddAllDependencies();
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var connection = @"Server=(localdb)\mssqllocaldb;Database=TestDB2;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<CheckersDbContext>(options => options.UseSqlServer(connection));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            /* // been here default
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });*/



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.ConfigureSwagger();
            app.UseHttpsRedirection();
            /*app.UseStaticFiles();
            app.UseCookiePolicy();*/
            app.UseCors(builder => builder.WithOrigins("http://localhost:4200").AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials() // essential for SignalR!!!
            );

            app.UseMvc();
        }
    }
}
