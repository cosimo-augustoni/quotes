using Microsoft.AspNetCore.Components;

namespace quotes_web.Shared
{
    public partial class MainLayout
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToAdmin()
        {
            this.NavigationManager.NavigateTo("admin");
        }

        private void NavigateToLogout()
        {
            this.NavigationManager.NavigateTo("logout");
        }

        private void NavigateToOverview()
        {
            this.NavigationManager.NavigateTo("");
        }
    }
}
