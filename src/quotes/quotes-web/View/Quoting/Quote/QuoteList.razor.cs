using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Quote;
using quotes_web.Persistence.Quoting;
using File = quotes_web.Persistence.Quoting.File;

namespace quotes_web.View.Quoting.Quote
{
    public partial class QuoteList
    {
        [Inject] 
        private IQuoteReadOnlyService QuoteReadOnlyService { get; set; } = default!;

        [Inject] 
        private IQuoteService QuoteService { get; set; } = default!;

        private IReadOnlyCollection<Persistence.Quoting.Quote> Quotes { get; set; } = new List<Persistence.Quoting.Quote>();

        [Parameter]
        public bool ShowDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LoadQuotesAsync();
            await base.OnInitializedAsync();
        }

        private async Task LoadQuotesAsync()
        {
            this.Quotes = await this.QuoteReadOnlyService.GetQuotesAsync();
        }

        private async Task DeleteQuoteAsync(Guid id)
        {
            await this.QuoteService.DeleteQuoteAsync(id);
            await this.LoadQuotesAsync();
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }
    }
}
