using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Quote
{
    public class QuoteReadOnlyService : IQuoteReadOnlyService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;

        public QuoteReadOnlyService(IDbContextFactory<QuotesContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IReadOnlyCollection<Persistence.Quoting.Quote>> GetQuotesAsync()
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            return await context.Quotes
                .Where(q => !q.DeletedAt.HasValue)
                .Include(q => q.Author)
                .ThenInclude(a =>  a!.File)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
