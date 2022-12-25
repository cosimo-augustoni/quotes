using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quotes_web.Domain;

namespace quotes_web.View.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration configuration;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(IConfiguration configuration, ILogger<LoginModel> logger)
        {
            this.configuration = configuration;
            this.logger = logger;
        }

        public async Task OnGet(string redirectUri)
        {
            var externalUrl = this.configuration.GetSection("Quotes").Get<QuotesConfiguration>()?.ExternalUrl;
            this.logger.LogInformation($"Using external URI for login redirect. URI: {externalUrl}");
            await this.HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = externalUrl != null ? $"{externalUrl}{redirectUri}" : redirectUri });
        }
    }
}
