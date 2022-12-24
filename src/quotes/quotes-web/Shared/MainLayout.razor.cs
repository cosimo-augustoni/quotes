using Blazorise;
using Microsoft.AspNetCore.Components;

namespace quotes_web.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;
        private Theme theme = new Theme
        {
        };
        private void NavigateToAdmin()
        {
            this.NavigationManager.NavigateTo("admin");
        }

        private void NavigateToLogout()
        {
            this.NavigationManager.NavigateTo("logout", forceLoad: true);
        }

        private void NavigateToOverview()
        {
            this.NavigationManager.NavigateTo("");
        }

        private void NavigateToLogin()
        {
            this.NavigationManager.NavigateTo("login?redirectUri=/", forceLoad: true );
        }
    }
}
