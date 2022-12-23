using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Quote;
using File = quotes_web.Persistence.Quoting.File;

namespace quotes_web.View.Quoting.Quote
{
    public partial class QuoteList
    {
        [Inject]
        private IQuoteReadOnlyService QuoteReadOnlyService { get; set; } = default!;

        [Inject]
        private IQuoteService QuoteService { get; set; } = default!;

        private ConfirmationModal? modalRef;
        private Guid quoteToDelete;

        private ICollection<Persistence.Quoting.Quote> Quotes { get; set; } = new List<Persistence.Quoting.Quote>();

        protected override async Task OnInitializedAsync()
        {
            await this.LoadQuotesAsync();
            await base.OnInitializedAsync();
        }

        private async Task LoadQuotesAsync()
        {
            var quotes = await this.QuoteReadOnlyService.GetQuotesAsync();
            this.Quotes = quotes.OrderByDescending(q => q.DateOfQuote).ToList();
            StateHasChanged();
        }

        private async Task DeleteQuoteAsync()
        {
            await this.QuoteService.DeleteQuoteAsync(quoteToDelete);
            await this.LoadQuotesAsync();
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }

        private Task ShowModal(Guid quoteId)
        {
            this.quoteToDelete = quoteId;
            return modalRef.ShowModal();
        }
    }
}
