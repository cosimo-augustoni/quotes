using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

namespace quotes_web.Domain.Authentication
{
    public static class AuthenticationDiExtension
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, ConfigurationManager configurationManager)
        {
            var oidcConfiguration = configurationManager.GetRequiredSection("OIDC").Get<OIDCConfiguration>() ?? throw new ArgumentException("OIDC konnte nicht ausgelesen werden");
            services.AddAuthentication(opt =>
                {
                    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddOpenIdConnect("oidc", options =>
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
                    options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

                    options.Events = new OpenIdConnectEvents
                    {
                        OnAccessDenied = context =>
                        {
                            context.HandleResponse();
                            context.Response.Redirect("/");
                            return Task.CompletedTask;
                        }
                    };

                    options.NonceCookie.SecurePolicy = CookieSecurePolicy.Always;
                    options.CorrelationCookie.SecurePolicy = CookieSecurePolicy.Always;
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy(QuotesPolicies.IsAdmin, policy => policy.RequireClaim("groups", "quotes-admin"));
                opt.AddPolicy(QuotesPolicies.IsUser, policy => policy.RequireClaim("groups", "quotes"));
            });

            return services;
        }
    }
}
