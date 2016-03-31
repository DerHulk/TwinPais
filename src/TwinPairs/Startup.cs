using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Authentication.Cookies;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Framework.ConfigurationModel;
using TwinPairs.Services;
using TwinPairs.ViewModels;

namespace TwinPairs
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder().AddJsonFile("App_Data\\config.json");
            this.Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, UserRole>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddSingleton<Microsoft.AspNet.Identity.ILookupNormalizer, Services.LookupNormilizer>();
            services.AddSingleton<Microsoft.AspNet.Identity.IPasswordHasher<ApplicationUser>, 
                                  Microsoft.AspNet.Identity.PasswordHasher<ApplicationUser>>();

            services.AddSingleton<Microsoft.AspNet.Identity.IUserClaimsPrincipalFactory<ApplicationUser>,
                                 Microsoft.AspNet.Identity.UserClaimsPrincipalFactory<ApplicationUser, UserRole>>();

            services.AddTransient(typeof(Microsoft.AspNet.Identity.RoleManager<UserRole>));
            services.AddTransient(typeof(Microsoft.AspNet.Identity.IdentityErrorDescriber));
            services.AddTransient(typeof(Microsoft.AspNet.Identity.UserManager<ApplicationUser>));
            services.AddTransient(typeof(Microsoft.AspNet.Identity.SignInManager<ApplicationUser>));

            services.AddScoped<Microsoft.AspNet.Identity.IUserStore<ApplicationUser>>((x)=> new Services.CustomUserStore<ApplicationUser>());
            services.AddScoped<Microsoft.AspNet.Identity.IRoleStore<UserRole>>((x) => new Services.RoleStore());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IHostingEnvironment env, IApplicationBuilder app)
        {
            //TemplateConfig(app, env, loggerFactory);
            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = "/account/login",
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true
            });
            app.UseGoogleAuthentication(x =>
            {
                x.ClientId = this.Configuration["client_id"];
                x.ClientSecret = this.Configuration["client_secret"];
                //x.SignInScheme = "Cookies";
                //x.AuthenticationScheme = "Google";
                //x.Scope.Add("email");
            });

            app.UseMvcWithDefaultRoute();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
