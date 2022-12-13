using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using quotes_web.Data;

namespace quotes_web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Configuration.AddJsonFile("/config.json");
            ConfigureServices(builder.Services, builder.Configuration);
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }


            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }

        public static void ConfigureServices(IServiceCollection services, ConfigurationManager configurationManager)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();


            var oidcConfiguration = configurationManager.GetRequiredSection("OIDC").Get<OIDCConfiguration>() ?? throw new ArgumentException("OIDC konnte nicht ausgelesen werden");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddCookie().AddOpenIdConnect("oidc", options =>
                {
                    options.Authority = oidcConfiguration.Authority;
                    options.ClientId = oidcConfiguration.ClientId;
                    options.ClientSecret = oidcConfiguration.ClientSecret;
                    options.ResponseType = "code";
                    options.SaveTokens = true;
                    options.GetClaimsFromUserInfoEndpoint = true;
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("groups");
                    options.TokenValidationParameters = new TokenValidationParameters{ NameClaimType = "name" };

                    options.Events = new OpenIdConnectEvents
                    {
                        OnAccessDenied = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/");
                            return Task.CompletedTask;
                        }
                    };
                });
        }

    }
}