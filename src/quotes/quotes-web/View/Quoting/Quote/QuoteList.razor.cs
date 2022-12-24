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

        private ConfirmationModal? modalRef;
        private Func<Task> subscription;
        private ICollection<Persistence.Quoting.Quote> Quotes { get; set; } = new List<Persistence.Quoting.Quote>();
        private const string PREVIOUS = "previous";
        private const string NEXT = "next";
        private string currentPage = "1";
        private const int SIZE = 10;
        private int PageItems => Quotes.Count / SIZE;
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

        private async Task DeleteQuoteAsync(Guid quoteId)
        {
            await this.QuoteService.DeleteQuoteAsync(quoteId);
            await this.LoadQuotesAsync();
            modalRef.OnConfirm -= subscription;
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }

        private Task ShowModal(Guid quoteId)
        {
            subscription = async () => await DeleteQuoteAsync(quoteId);
            modalRef.OnConfirm += subscription;
            return modalRef.ShowModal();
        }
        private bool IsActive(string page)
         => currentPage == page;
        private bool IsPageNavigationDisabled(string navigation)
        {
            if (navigation.Equals(PREVIOUS))
            {
                return currentPage.Equals("1");
            }
            else if (navigation.Equals(NEXT))
            {
                return currentPage.Equals(PageItems.ToString());
            }
            return false;
        }

        private void Previous()
        {
            var currentPageAsInt = int.Parse(currentPage);
            if (currentPageAsInt > 1)
            {
                currentPage = (currentPageAsInt - 1).ToString();
            }
        }

        private void Next()
        {
            var currentPageAsInt = int.Parse(currentPage);
            if (currentPageAsInt < PageItems)
            {
                currentPage = (currentPageAsInt + 1).ToString();
            }
        }

        private void SetActive(string page)
            => currentPage = page;
    }
}
