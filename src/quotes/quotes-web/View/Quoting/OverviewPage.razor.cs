using Microsoft.AspNetCore.Components;

namespace quotes_web.View.Quoting
{
    public partial class OverviewPage : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private void NavigateToQuoteCreation()
        {
            this.NavigationManager.NavigateTo("quote/create");
        }

        private void NavigateToAuthorCreation()
        {
            this.NavigationManager.NavigateTo("author/create");
        }
    }
}
