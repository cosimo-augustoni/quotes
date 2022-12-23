using Microsoft.AspNetCore.Components.Forms;
using quotes_web.Data.Import;
using System.Text.Json;

namespace quotes_web.View.Administration
{
    public partial class ImportForm
    {
        private async Task Import(InputFileChangeEventArgs e)
        {
            using var stream = new MemoryStream();
            await e.File.OpenReadStream().CopyToAsync(stream);
            stream.Seek(0, SeekOrigin.Begin);

            var importContainer = JsonSerializer.Deserialize<ImportContainer>(stream);
            if (importContainer?.Authors != null)
                this.QuotesContext.Authors.AddRange(importContainer.Authors);

            if (importContainer?.Quotes != null)
                this.QuotesContext.Quotes.AddRange(importContainer.Quotes);

            await this.QuotesContext.SaveChangesAsync();
        }
    }
}
