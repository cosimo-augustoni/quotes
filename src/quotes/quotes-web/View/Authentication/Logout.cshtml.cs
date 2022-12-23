using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace quotes_web.View.Authentication
{
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            await this.HttpContext.SignOutAsync();
            return this.Redirect("/");
        }
    }
}
