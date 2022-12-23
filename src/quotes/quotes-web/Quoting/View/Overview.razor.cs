using Microsoft.AspNetCore.Components;

namespace quotes_web.Quoting.View
{
    public partial class Overview : ComponentBase
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
