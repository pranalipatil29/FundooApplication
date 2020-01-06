// ******************************************************************************
//  <copyright file="Startup.cs" company="Bridgelabz">
//    Copyright © 2019 Company
//
//     Execution:  Startup.cs
//  
//     Purpose:  configure services and the app's request pipeline.
//     @author  Pranali Patil
//     @version 1.0
//     @since   18-12-2019
//  </copyright>
//  <creator name="Pranali Patil"/>
// ******************************************************************************
namespace FundooApp
{
    // Including the requried assemblies in to the program
    using System;
    using System.Collections.Generic;
    using System.Text;
    using FundooBusinessLayer.InterfaceBL;
    using FundooBusinessLayer.ServicesBL;
    using FundooBusinessLayer1.InterfaceBL;
    using FundooBusinessLayer1.ServicesBL;
    using FundooCommonLayer.Model;
    using FundooRepositoryLayer.Context;
    using FundooRepositoryLayer.InterfaceRL;
    using FundooRepositoryLayer.ServiceRL;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.Facebook;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Swashbuckle.AspNetCore.Swagger;

    /// <summary>
    ///  defines the services
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IAccountBL, AccountBL>();
            services.AddTransient<IAccountRL, AccountRL>();

            services.AddTransient<INoteBL, NoteBL>();
            services.AddTransient<INoteRL, NoteRL>();

            services.AddTransient<ILabelBL, LabelBL>();
            services.AddTransient<ILabelRL, LabelRL>();

            services.AddTransient<IAdminBL, AdminBL>();
            services.AddTransient<IAdminRL, AdminRL>();

            services.Configure<ApplicationSetting>(this.Configuration.GetSection("ApplicationSetting"));

            services.AddDbContext<AuthenticationContext>(Options =>
           Options.UseSqlServer(this.Configuration.GetConnectionString("IdentityConnection")));

            services.AddDefaultIdentity<ApplicationModel>().AddEntityFrameworkStores<AuthenticationContext>();

            var key = Encoding.UTF8.GetBytes(this.Configuration["ApplicationSetting:JWTSecret"].ToString());

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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Fundoo API", Description = "Swagger Core API" });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Swagger",
                    Name = "Authorization",
                    In = "header",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                    {
                        "Bearer", new string[] { } }
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //services.AddAuthentication(Options =>
            //{
            //    Options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            //    Options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //    Options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            //})

            ////// Adding the AppId and AppSecret key
            //.AddFacebook(Options =>
            //{
            //    Options.AppId = Configuration["ApplicationSetting:FacebookAppId"];
            //    Options.AppSecret = Configuration["ApplicationSetting:FacebookAppSecret"];
            //}).AddCookie();


        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
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

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fundoo API");
            });
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
