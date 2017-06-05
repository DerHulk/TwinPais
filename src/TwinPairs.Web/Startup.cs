using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TwinPairs.Web.Services;
using TwinPairs.Web.ViewModels;

namespace TwinPairs.Web
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
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, UserRole>().AddDefaultTokenProviders();
            services.AddMvc();
            services.AddSingleton<ILookupNormalizer, Services.LookupNormilizer>();
            services.AddSingleton<IPasswordHasher<ApplicationUser>,
                                  PasswordHasher<ApplicationUser>>();

            services.AddSingleton<IUserClaimsPrincipalFactory<ApplicationUser>,
                                  UserClaimsPrincipalFactory<ApplicationUser, UserRole>>();

            services.AddTransient(typeof(RoleManager<UserRole>));
            services.AddTransient(typeof(IdentityErrorDescriber));
            services.AddTransient(typeof(UserManager<ApplicationUser>));
            services.AddTransient(typeof(SignInManager<ApplicationUser>));

            services.AddScoped<Microsoft.AspNetCore.Identity.IUserStore<ApplicationUser>>((x) => new Services.CustomUserStore<ApplicationUser>());
            services.AddScoped<Microsoft.AspNetCore.Identity.IRoleStore<UserRole>>((x) => new Services.RoleStore());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TemplateConfig(app, env, loggerFactory);
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseIdentity();
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                LoginPath = "/account/login",
                AuthenticationScheme = "Cookies",
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                ExpireTimeSpan = new TimeSpan(1, 0, 0, 0, 0),
                Events = new CookieAuthenticationEvents()
                {
                    OnRedirectToLogin = ctx =>
                    {
                        if (ctx.Request.Headers["TwinPairs.Api"] == "V1.0" &&
                            ctx.Response.StatusCode == (int)HttpStatusCode.OK)
                        {
                            ctx.Response.Clear();
                            ctx.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                        }
                        else
                        {
                            ctx.Response.Redirect(ctx.RedirectUri);
                        }
                        return Task.FromResult(0);
                    }
                }
            });

            var googleOptions = new GoogleOptions();
            googleOptions.ClientId = this.Configuration["client_id"];
            googleOptions.ClientSecret = this.Configuration["client_secret"];

            app.UseGoogleAuthentication(googleOptions);
            app.UseMvcWithDefaultRoute();

        }
    }
}
