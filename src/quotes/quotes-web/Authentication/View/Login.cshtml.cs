using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace quotes_web.Authentication.View
{
    public class LoginModel : PageModel
    {
        public async Task OnGet(string redirectUri)
        {
            await this.HttpContext.ChallengeAsync("oidc", new AuthenticationProperties { RedirectUri = "/" });
        }
    }
}
