using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Quote
{
    internal class QuoteService : IQuoteService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;

        public QuoteService(IDbContextFactory<QuotesContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task AddQuoteAsync(QuoteCreation quoteCreation)
        {
            //TODO Validation
            var quote = new Persistence.Quoting.Quote
            {
                Id = Guid.NewGuid(),
                Text = quoteCreation.Text,
                DateOfQuote = quoteCreation.DateOfQuote.Value,
                AuthorId = quoteCreation.AuthorId.Value,
            };

            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            await context.Quotes.AddAsync(quote);
            await context.SaveChangesAsync();
        }

        public async Task DeleteQuoteAsync(Guid quoteId)
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var quoteToDelete = await context.Quotes.FindAsync(quoteId);
            if (quoteToDelete == null) 
                return;

            context.Quotes.Remove(quoteToDelete);
            await context.SaveChangesAsync();
        }
    }
}
