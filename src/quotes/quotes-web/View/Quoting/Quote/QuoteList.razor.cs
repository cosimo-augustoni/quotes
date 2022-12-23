using Microsoft.AspNetCore.Components;
using quotes_web.Data;

namespace quotes_web.View.Quoting.Quote
{
    public partial class QuoteList
    {
        [Inject] 
        private QuotesContext QuotesContext { get; set; } = default!;

        [Parameter]
        public bool ShowDelete { get; set; }

        private void DeleteQuote(Guid id)
        {
            var quote = this.QuotesContext.Quotes.Find(id);
            this.QuotesContext.Remove(quote);
            this.QuotesContext.SaveChanges();
        }
    }
}
