using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;



using WebApi2.Models;
using Core.Repositories;
using Core.Services;
using Core.Repositories.Impl;
using Core.Services.Impl;
using Core;

namespace WebApi2
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
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                  .AddJwtBearer(options =>
                  {
                      options.RequireHttpsMetadata = false;
                      options.TokenValidationParameters = new TokenValidationParameters
                      {
                            // укзывает, будет ли валидироваться издатель при валидации токена
                           ValidateIssuer = false,
                            // строка, представляющая издателя
                            //ValidIssuer = AuthOptions.ISSUER,

                            // будет ли валидироваться потребитель токена
                           ValidateAudience = false,
                            // установка потребителя токена
                           // ValidAudience = AuthOptions.AUDIENCE,
                            // будет ли валидироваться время существования
                            ValidateLifetime = true,

                            // установка ключа безопасности
                            IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                            // валидация ключа безопасности
                            ValidateIssuerSigningKey = true,
                      };
                  });
            string connection = Configuration.GetConnectionString("RadioConnection");
            services.AddDbContext<RadioContext>(options => options.UseSqlServer(connection));

            services.AddScoped<IEmployerRepository, EmployerRepository>();
            services.AddScoped<IReleaseRepository, ReleaseRepository>();
            services.AddScoped<IReclameBlockRepository, ReclameBlockRepository>();

            services.AddScoped<IEmployerService, EmployerService>();
            services.AddScoped<IReleaseService, ReleaseService>();
            services.AddScoped<IReclameBlockService, ReclameBlockService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            //app.UseMvc();
            //app.UseRouting();

            app.UseAuthentication();
            app.UseMvc();
            //app.UseAuthorization();

            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });*/

        }
    }
}
