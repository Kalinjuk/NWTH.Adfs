using ADFS;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using ADFS.APIKeyAuth;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using nwth.ADFS;

namespace TestADFS
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
            services.AddControllersWithViews();
           //services.AddTransient<IAdfsAuthProcessor,TestAdfsProcessor>();// < .AddTransient<>



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = "ADFS";

            })

            .AddCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromSeconds(60 * 60 * 8); //Кука работает 8 часов. Надо заменить на время авторизации ADFS
                                                                            //options.Cookie.SecurePolicy = new CookieSecurePolicy()
                options.Cookie.SameSite = SameSiteMode.Strict;
            })
            .AddADFS("ADFS",null)
            /*, options =>
            {
                options.Server = "oa2.220v.ru";
                options.ClientId = "8e6604b9-c270-47fd-9b47-2df822bedac5";
                options.ClientSecret = "6dJxjFBH2wJdrWqDfujUfH8kDf9QLQaDbyaUu7ke";
                options.Resource = "TestApp";
                options.CorrelationCookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Strict;
                options.AccessDeniedPath = "/home/accessDenied";
            })*/
            .AddApiKeySupport(options => { });

            services.Configure<ADFSOptions>("ADFS",Configuration.GetSection("ADFSOptions"));

           // services.AddScoped<ADFS.IAdfsAuthProcessor, ADFSLoginProcessor>();
            services.AddSingleton<IGetApiKeyQuery, ApiKeyQuery>();
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
