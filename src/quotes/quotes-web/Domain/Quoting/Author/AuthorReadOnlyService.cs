using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Author
{
    internal class AuthorReadOnlyService : IAuthorReadOnlyService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;

        public AuthorReadOnlyService(IDbContextFactory<QuotesContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task<IReadOnlyCollection<Persistence.Quoting.Author>> GetAuthorsAsync()
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            return await context.Authors
                .Include(a => a.File)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
