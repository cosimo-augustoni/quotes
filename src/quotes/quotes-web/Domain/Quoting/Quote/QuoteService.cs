using Blazored.Toast.Services;
using Microsoft.EntityFrameworkCore;
using quotes_web.Data;
using quotes_web.Domain.Quoting.Author;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Quote
{
    internal class QuoteService : IQuoteService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;
        private readonly IToastService toastService;

        public QuoteService(IDbContextFactory<QuotesContext> dbContextFactory, IToastService toastService)
        {
            this.dbContextFactory = dbContextFactory;
            this.toastService = toastService;
        }

        public async Task<bool> AddQuoteAsync(QuoteCreation quoteCreation)
        {
            if (!quoteCreation.Validate(out var messages))
            {
                foreach (var message in messages)
                {
                    this.toastService.ShowError(message.Text, message.Title);
                }
                return false;
            }
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
            return true;
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
