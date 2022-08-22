using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AspnetRunBasics {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services.AddHttpClient<ICatalogService, CatalogService>(
                x => x.BaseAddress = new Uri(Configuration["ApiSettings:APIGW"]));
            services.AddHttpClient<IBasketService, BasketService>(
                x => x.BaseAddress = new Uri(Configuration["ApiSettings:APIGW"]));
            services.AddHttpClient<IOrderService, OrderService>(
                x => x.BaseAddress = new Uri(Configuration["ApiSettings:APIGW"]));

            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) 
                app.UseDeveloperExceptionPage();
            else {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapRazorPages();
            });
        }
    }
}
