using quotes_web.Data;

namespace quotes_web.Domain.Quoting.Quote
{
    public interface IQuoteService
    {
        /// <returns>If Successful</returns>
        Task<bool> AddQuoteAsync(QuoteCreation quoteCreation);
        Task DeleteQuoteAsync(Guid quoteId);
    }
}