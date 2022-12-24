using Blazorise;
using Microsoft.EntityFrameworkCore;
using quotes_web.Persistence.Quoting;
using System.Text.Json;

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
            var importContainer = JsonSerializer.Deserialize<ImportExportContainer>(stream);
            if (importContainer?.Authors != null)
                context.Authors.AddRange(importContainer.Authors);

            if (importContainer?.Quotes != null)
                context.Quotes.AddRange(importContainer.Quotes);

            await context.SaveChangesAsync();
        }

        public async Task<Stream> ExportAsync()
        {
            await using var context = await this.dbContextFactory.CreateDbContextAsync();
            var container =  new ImportExportContainer
            {
                Authors = await context.Authors.Include(a => a.File).AsNoTracking().ToListAsync(),
                Quotes = await context.Quotes.AsNoTracking().ToListAsync()
            };

            var stream = new MemoryStream();
            await JsonSerializer.SerializeAsync(stream, container);
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }

        private class ImportExportContainer
        {
            public List<Author>? Authors { get; set; }

            public List<Quote>? Quotes { get; set; }
        }
    }
}
