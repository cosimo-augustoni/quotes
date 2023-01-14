using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace quotes_web.Shared
{
    public partial class MainLayout
    {
        private MudTheme mudTheme  = new MudTheme()
        {
            Palette = new Palette()
            {
                AppbarBackground = Colors.BlueGrey.Lighten1
            },
        };

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

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
            this.NavigationManager.NavigateTo("login?redirectUri=/", forceLoad: true);
        }

        private bool isDarkMode;
        private MudThemeProvider? mudThemeProvider;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (this.mudThemeProvider != null)
                {
                    this.isDarkMode = await this.mudThemeProvider.GetSystemPreference();
                    this.StateHasChanged();
                }
            }
        }
    }
}
