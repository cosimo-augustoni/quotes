using Microsoft.AspNetCore.Components.Forms;

namespace quotes_web.Domain.ImportExport
{
    public interface IImportExportService
    {
        Task ImportAsync(IBrowserFile file);
        Task<Stream> ExportAsync();
    }
}