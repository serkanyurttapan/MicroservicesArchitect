using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCWeb.Extensions;
using MVCWeb.Helper;
using MVCWeb.Helper.Handler;
using MVCWeb.Models;
using MVCWeb.Services;
using MVCWeb.Services.Interfaces;
using MVCWeb.Validator;
using Shared.Service;
using System;

namespace MVCWeb
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
            //services.AddHttpContextAccessor();
            //services.AddScoped<ISharedIdentityService, SharedIdentityService>();
            //services.Configure<ServiceApiSettings>(Configuration.GetSection("ServiceApiSettings"));
            //services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            //services.AddAccessTokenManagement();

            //services.AddSingleton<PhotoHelper>();
            //services.AddScoped<ResourceOwnerPasswordTokenHandler>();
            //services.AddScoped<ClientCredentialTokenHandler>();

            #region ***service collectionlar ServiceExtension static sýnýfýna taþýndý***
            //var serviceApiSettings = Configuration.GetSection("ServiceApiSettings").Get<ServiceApiSettings>();
            //services.AddHttpClient<IIdentiyService, IdentityService>();
            //services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();

            ////AddHttpMessageHandler ile her istek yapýldýðýnda delegate çalýþýyor.
            //services.AddHttpClient<IUserService, UserService>(opt =>
            //{
            //    opt.BaseAddress = new Uri(serviceApiSettings.IdentityBaseUri);
            //}).AddHttpMessageHandler<ResourceOwnerPasswordTokenHandler>();

            //services.AddHttpClient<ICatalogService, CatalogService>(opt =>
            // {
            //     opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Catalog.Path}");
            // }).AddHttpMessageHandler<ClientCredentialTokenHandler>();


            //services.AddHttpClient<IPhotoStockService, PhotoStockService>(opt =>
            //{
            //    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.PhotoStock.Path}");
            //}).AddHttpMessageHandler<ClientCredentialTokenHandler>();

            //services.AddHttpClient<IBasketService, BasketService>(opt =>
            //{
            //    opt.BaseAddress = new Uri($"{serviceApiSettings.GatewayBaseUri}/{serviceApiSettings.Basket.Path}");

            //}).AddHttpMessageHandler<ClientCredentialTokenHandler>();
            #endregion

            services.AddHttpClientServices(Configuration);

            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt =>
            // {
            //     opt.LoginPath = "/Auth/SignIn";
            //     opt.ExpireTimeSpan = TimeSpan.FromDays(60);
            //     opt.SlidingExpiration = true;
            //     opt.Cookie.Name = "microservicewebcookie";
            // });

            //services.AddControllersWithViews().AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<CourseCreateInputValidator>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
