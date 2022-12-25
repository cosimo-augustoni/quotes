using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;
using quotes_web.Domain;

namespace quotes_web.View.Authentication
{
    public class LoginModel : PageModel
    {
        private readonly IConfiguration configuration;

        public LoginModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task OnGet(string redirectUri)
        {
            var externalUrl = this.configuration.GetSection("Quotes").Get<QuotesConfiguration>()?.ExternalUrl;
            await this.HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = externalUrl != null ? $"{externalUrl}{redirectUri}" : redirectUri });
        }
    }
}
