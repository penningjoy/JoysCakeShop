using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JoysCakeShop.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace JoysCakeShop
{
    public class Startup
    {
        // Using IConfiguration interface, the startup class is going to 
        // access the configuration values set in the appsetting.json file.
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime.Add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // register services here through Dependency Injection
            services.AddControllersWithViews();
            services.AddDbContext<CakesDbContext>(options =>
                        options.UseSqlServer(Configuration.GetConnectionString("CakesDatabaseConnection")));

            // Adds basic functionality to work with Identity; Framework stores is added to show 
            // that the idenity data will be stored using the CakesDbContext.

            services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<CakesDbContext>(); 
            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddRazorPages(); // needed for scaffolded files -> we did for identity

            //Custom Services
            services.AddScoped<ICakeRepository, CakeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp)); // This also uses the factory design pattern -> GetCart() here is the factory method

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();  // This will show detailed errors useful for development
                
            }
            app.UseHttpsRedirection(); // Any request over http will be redirected to https
            app.UseStatusCodePages();  // For the app to return status codes . e.g.- 404 status code.
            app.UseStaticFiles();      // For serving static files
            app.UseSession();          // For using session variable. The usesession() must come before routing. 
                                       //The order needs to be maintained. 
           
            app.UseRouting();                 // For routing 
            app.UseAuthentication();         // Use Authentication 
            app.UseAuthorization();          // Use Authorization 
            app.UseEndpoints(endpoints =>   //Special asp.net core feature to define the pattern for link(url) resolution
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}"
                );

                endpoints.MapRazorPages(); //For Razor Pages
            });

        }
    }
}


/*  ***************************   Notes  *********************************************/


/*            --------- Startup of an Application -----------
 *  Remarks:
 *  
 *  Program Class  -> StartUp class -> ConfigureServices() method to register services  
 *                                                      |
 *                                                      v
 *  Application is now ready for requests <- Configure method to create a pipeline
 *
 * 
 * Identity API--
 *  -- Identity DBContext Inherited by the CakeDBContext
 *  -- Configuration Changes
 *     -- Password Length
 *     -- Cookies
 *     -- User Options
 *     -- 
 * *********************************************************************************/