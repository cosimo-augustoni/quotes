using Microsoft.AspNetCore.Components;
using MudBlazor;
using quotes_web.Domain.Quoting.Quote;
using quotes_web.View.Shared;
using File = quotes_web.Persistence.Quoting.File;

namespace quotes_web.View.Quoting.Quote
{
    public partial class QuoteList
    {
        [Inject]
        private IQuoteReadOnlyService QuoteReadOnlyService { get; set; } = default!;

        [Inject]
        private IQuoteService QuoteService { get; set; } = default!;

        [Inject]
        private IDialogService DialogService { get; set; } = default!;

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
            this.StateHasChanged();
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }

        private async Task ConfirmDelete(Guid quoteId)
        {
            var dialog = await this.DialogService.ShowAsync<ConfirmDeleteDialog>("Löschen");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await this.QuoteService.DeleteQuoteAsync(quoteId);
                await this.LoadQuotesAsync();
            }
        }
    }
}
