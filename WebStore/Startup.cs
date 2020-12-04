using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebStore.Infrastructure.Conventions;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Middleware;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        private readonly IConfiguration _Configuration;

        public Startup(IConfiguration Configuration) => _Configuration = Configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddTransient<IService, ServiceImplementation>();
            //services.AddScoped<IService, ServiceImplementation>();
            //services.AddSingleton<IService, ServiceImplementation>();

            services.AddTransient<IEmployeesData, InMemoryEmployeesData>();
            //services.AddTransient<IEmployeesData>(service => new InMemoryEmployeesData());

            //services.AddMvc(opt => opt.Conventions.Add(new WebStoreControllerConvention()));
            services
               .AddControllersWithViews(opt =>
                {
                    //opt.Conventions.Add(new WebStoreControllerConvention());
                })
               .AddRazorRuntimeCompilation();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env/*, IServiceProvider services*/)
        {
            //var employees = services.GetService<IEmployeesData>();

            //using (var scope = services.CreateScope())
            //{
            //    var service = scope.ServiceProvider.GetRequiredService<IEmployeesData>();
            //}
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }

            app.UseStaticFiles();

            app.UseRouting();

            //app.UseMiddleware<TestMiddleware>();
            //app.UseMiddleware(typeof(TestMiddleware));

            //app.Map(
            //    "/Hello", 
            //    context => context.Run(async request => await request.Response.WriteAsync("Hello World!")));

            //app.MapWhen(
            //    context => context.Request.Query.ContainsKey("id") && context.Request.Query["id"] == "5",
            //    context => context.Run(async request => await request.Response.WriteAsync("Hello World with id:5!")));

            app.UseWelcomePage("/welcome");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/greetings", async ctx => await ctx.Response.WriteAsync(_Configuration["greetings"]));
                endpoints.MapGet("/HelloWorld", async ctx => await ctx.Response.WriteAsync("Hello World!"));

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // http://localhost:5000 -> controller == "Home" action == "Index"
                // http://localhost:5000/Products -> controller == "Products" action == "Index"
                // http://localhost:5000/Products/Page -> controller == "Products" action == "Page"
                // http://localhost:5000/Products/Page/5 -> controller == "Products" action == "Page" id = "5"
            });
        }
    }
}
