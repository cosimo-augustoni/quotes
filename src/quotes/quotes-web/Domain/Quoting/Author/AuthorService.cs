using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;

        public AuthorService(IDbContextFactory<QuotesContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task AddAuthorAsync(AuthorCreation authorCreation)
        {
            //TODO Validation
            var fileCreation = authorCreation.FileCreation;
            var file = new quotes_web.Persistence.Quoting.File
            {
                Id = Guid.NewGuid(),
                Name = fileCreation.Name,
                FileType = fileCreation.FileType
            };
            var author = new Persistence.Quoting.Author
            {
                Id = Guid.NewGuid(),
                Name = authorCreation.Name,
                FileId = file.Id,
            };

            await using var fileStream = System.IO.File.Create(file.FilePath);
            await authorCreation.FileCreation.FileEntry.OpenReadStream(authorCreation.FileCreation.FileEntry.Size).CopyToAsync(fileStream);

            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            await context.AddAsync(file);
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAuthorAsync(Guid authorId)
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var authorToDelete = await context.Authors.FindAsync(authorId);
            if (authorToDelete == null)
                return;

            context.Authors.Remove(authorToDelete);
            await context.SaveChangesAsync();
        }
    }
}
