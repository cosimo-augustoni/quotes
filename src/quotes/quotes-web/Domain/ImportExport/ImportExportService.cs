using Blazorise;
using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;
using System.Text.Json;
using File = quotes_web.Persistence.Quoting.File;

namespace quotes_web.Domain.ImportExport
{
    public class ImportExportService : IImportExportService
    {
        private readonly IDbContextFactory<QuotesContext> dbContextFactory;

        public ImportExportService(IDbContextFactory<QuotesContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public async Task ImportAsync(IFileEntry file)
        {
            using var stream = new MemoryStream();
            await file.OpenReadStream().CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var importContainer = JsonSerializer.Deserialize<ImportContainer>(stream);
            if (importContainer?.Authors != null)
            {
                foreach (var importAuthor in importContainer.Authors)
                {
                    var image = importAuthor.File ?? new File { FileType = "image/png", Id = Guid.NewGuid(), Name = "NoImage" };
                    var author = new Author
                    {
                        Id = importAuthor.Id ?? throw new ArgumentException("Id is Null"),
                        Name = importAuthor.Name ?? throw new ArgumentException("Name is Null"),
                        File = image,
                        FileId = image.Id
                    };
                    context.Authors.Add(author);
                }
            }

            if (importContainer?.Quotes != null)
                context.Quotes.AddRange(importContainer.Quotes);

            await context.SaveChangesAsync();
        }

        public async Task<Stream> ExportAsync()
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var container =  new ExportContainer
            {
                Authors = await context.Authors.Include(a => a.File).AsNoTracking().ToListAsync(),
                Quotes = await context.Quotes.AsNoTracking().ToListAsync()
            };

            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, container);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private class ImportContainer
        {
            public List<ImportAuthor>? Authors { get; set; }

            public List<Quote>? Quotes { get; set; }
        }

        private class ExportContainer
        {
            public List<Author>? Authors { get; set; }

            public List<Quote>? Quotes { get; set; }
        }

        private class ImportAuthor
        {
            public Guid? Id { get; init; }

            public string? Name { get; init; }

            public Guid? FileId { get; init; }

            public  File? File { get; set; }
        }
    }
}
