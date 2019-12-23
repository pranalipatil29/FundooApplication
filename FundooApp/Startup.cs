using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundooBusinessLayer1.InterfaceBL;
using FundooBusinessLayer1.ServicesBL;
using FundooCommonLayer.Model;
using FundooRepositoryLayer.Context;
using FundooRepositoryLayer.InterfaceRL;
using FundooRepositoryLayer.ServiceRL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace FundooApp
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

            services.AddTransient<IAccountBL, AccountBL>();
            services.AddTransient<IAccountRL, AccountRL>();

            services.Configure<ApplicationSetting>(Configuration.GetSection("ApplicationSetting"));

            //services.AddDbContext<AuthenticationContext>(option => option.UseSqlServer(Configuration["ConnectionString:IdentityConnection"]));

            services.AddDbContext<AuthenticationContext>(Options =>
           Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationModel>().AddEntityFrameworkStores<AuthenticationContext>();

            var key = Encoding.UTF8.GetBytes(Configuration["ApplicationSetting:JWTSecret"].ToString());

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;

                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title="Fundoo API", Description="Swagger Core API"});
            });
        }


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

           
            app.UseAuthentication();            
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fundoo API");
            });
        }
    }
}
