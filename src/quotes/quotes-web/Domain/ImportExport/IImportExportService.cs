using Blazorise;

namespace quotes_web.Domain.ImportExport
{
    public interface IImportExportService
    {
        Task ImportAsync(IFileEntry file);
        Task<Stream> ExportAsync();
    }
}