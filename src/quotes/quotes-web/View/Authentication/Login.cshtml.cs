using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
            await this.HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}
