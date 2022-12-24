using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using quotes_web.Domain.ImportExport;
using System.Globalization;

namespace quotes_web.View.Administration
{
    public partial class ImportExportForm
    {
        [Inject] 
        private IImportExportService ImportExportService { get; set; } = default!;

        [Inject] 
        private IJSRuntime JsRuntime { get; set; } = default!;


        private async Task Import(FileChangedEventArgs arg)
        {
            var file = arg.Files.FirstOrDefault();
            if (file == null)
                return;

            await this.ImportExportService.ImportAsync(file);
        }

        private async Task ExportAsync()
        {
            var stream = await this.ImportExportService.ExportAsync();
            var fileName = $"quotes_export_{DateTime.Now.ToString(CultureInfo.CurrentCulture)}.json";

            using var streamRef = new DotNetStreamReference(stream: stream);
            await this.JsRuntime.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
        }
    }
}
