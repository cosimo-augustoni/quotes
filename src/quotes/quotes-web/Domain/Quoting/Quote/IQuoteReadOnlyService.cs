namespace quotes_web.Domain.Quoting.Quote
{
    public interface IQuoteReadOnlyService
    {
        Task<IReadOnlyCollection<Persistence.Quoting.Quote>> GetQuotesAsync();
    }
}