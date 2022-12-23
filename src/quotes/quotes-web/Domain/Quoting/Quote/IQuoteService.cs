using quotes_web.Data;

namespace quotes_web.Domain.Quoting.Quote
{
    public interface IQuoteService
    {
        Task AddQuoteAsync(QuoteCreation quoteCreation);
        Task DeleteQuoteAsync(Guid quoteId);
    }
}