using Microsoft.AspNetCore.Components;
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

        private QuoteCreation quoteCreation = new QuoteCreation();

        private IReadOnlyCollection<Persistence.Quoting.Author> Authors { get; set; } = new List<Persistence.Quoting.Author>();

        protected override async Task OnInitializedAsync()
        {
            this.Authors = await this.AuthorReadOnlyService.GetAuthorsAsync();
            await base.OnInitializedAsync();
        }

        private async Task CreateQuote()
        {
            if (await this.QuoteService.AddQuoteAsync(this.quoteCreation))
            {
                this.NavigationManager.NavigateTo("/");
            }
        }
    }
}
