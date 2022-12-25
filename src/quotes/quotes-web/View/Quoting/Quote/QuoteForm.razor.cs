using Microsoft.AspNetCore.Components;
using Microsoft.Identity.Client;
using quotes_web.Domain.Quoting.Author;
using quotes_web.Domain.Quoting.Quote;

namespace quotes_web.View.Quoting.Quote
{
    public partial class QuoteForm
    {
        [Inject] 
        private IAuthorReadOnlyService AuthorReadOnlyService { get; set; } = default!;

        [Inject] 
        private IQuoteService QuoteService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private QuoteCreation quoteCreation = new QuoteCreation(){AuthorId = null};
        private bool loading;
        private Guid? SelectedAuthor { get; set; }

        private IReadOnlyCollection<Persistence.Quoting.Author> Authors { get; set; } = new List<Persistence.Quoting.Author>();

        protected override async Task OnInitializedAsync()
        {
            this.Authors = await this.AuthorReadOnlyService.GetAuthorsAsync();
            await base.OnInitializedAsync();
        }

        private async Task CreateQuote()
        {
            loading = true;
            if (await this.QuoteService.AddQuoteAsync(this.quoteCreation))
            {
                this.NavigationManager.NavigateTo("/");
            }
            loading = false;
        }
    }
}
