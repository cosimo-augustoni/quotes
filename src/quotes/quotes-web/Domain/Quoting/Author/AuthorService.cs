using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;

namespace quotes_web.Domain.Quoting.Author
{
    public class AuthorService : IAuthorService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;
        private readonly IImageFileService imageFileService;
        private readonly IToastService toastService;

        public AuthorService(IDbContextFactory<QuotesContext> dbContextFactory, IImageFileService imageFileService, IToastService toastService)
        {
            this.dbContextFactory = dbContextFactory;
            this.imageFileService = imageFileService;
            this.toastService = toastService;
        }

        public async Task<bool> AddAuthorAsync(AuthorCreation authorCreation)
        {
            if (!authorCreation.Validate(out var messages))
            {
                foreach (var message in messages)
                {
                    this.toastService.ShowError(message.Text, message.Title);
                }
                return false;
            }

            var fileCreation = authorCreation.FileCreation;
            var file = new quotes_web.Persistence.Quoting.File
            {
                Id = Guid.NewGuid(),
                Name = fileCreation.Name!,
                FileType = fileCreation.FileType!
            };
            var author = new Persistence.Quoting.Author
            {
                Id = Guid.NewGuid(),
                Name = authorCreation.Name!,
                FileId = file.Id,
            };
            await this.imageFileService.CreateImageFileAsync(file.FilePath, authorCreation.FileCreation.FileEntry!);

            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            await context.AddAsync(file);
            await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task UpdateAuthorImageAsync(Guid authorId, IBrowserFile? fileEntry)
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var author = await context.Authors.Include(a => a.File).FirstOrDefaultAsync(a => a.Id == authorId);
            if (author?.File == null || fileEntry == null)
                return;

            await this.imageFileService.UpdateImageFileAsync(author.File.FilePath, fileEntry);
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
