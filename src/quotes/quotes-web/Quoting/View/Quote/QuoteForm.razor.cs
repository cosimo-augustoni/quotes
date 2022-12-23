using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Data;

namespace quotes_web.Quoting.View.Quote
{
    public partial class QuoteForm
    {
        [Inject] 
        private QuotesContext QuotesContext { get; set; } = default!;

        private Validations? validations;
        private QuoteCreation quoteCreation = new QuoteCreation();
        private IEnumerable<Data.Author> authors;
        protected override void OnInitialized()
        {
            this.authors = this.QuotesContext.Authors.ToList();
            this.StateHasChanged();
        }

        private async Task CreateQuote()
        {
            if (await this.validations.ValidateAll())
            {
                this.QuotesContext.Add(new Data.Quote
                {
                    Text = this.quoteCreation.Text,
                    AuthorId = this.quoteCreation.AuthorId.Value,
                    DateOfQuote = this.quoteCreation.DateOfQuote.Value,
                    Id = Guid.NewGuid(),
                });
                await this.QuotesContext.SaveChangesAsync();
            }
        }
    }
}
