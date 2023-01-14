using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using quotes_web.Domain.Quoting.Author;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorForm
    {
        [Inject]
        private IToastService ToastService { get; set; } = default!;
     
        [Inject]
        private IAuthorService AuthorService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        [Inject]
        private IImageFileService ImageFileService { get; set; } = default!;

        private bool loading;
        private AuthorCreation authorCreation = new AuthorCreation();

        private async Task CreateAuthorAsync()
        {
            this.loading = true;
            if (await this.AuthorService.AddAuthorAsync(this.authorCreation))
            {
                this.ToastService.ShowSuccess("Author wurde erstellt.", "Erstellen erfolgreich!");
                this.NavigationManager.NavigateTo("/");
            }
            this.loading = false;

        }

        private void OnChanged(IBrowserFile file)
        {
            var fileCreation = this.ImageFileService.GetFileCreationFromBrowserFile(file);
            if (fileCreation == null)
                return;

            this.authorCreation.FileCreation = fileCreation;
        }

        public string Humanize(long? byteSize)
        {
            if (byteSize == null)
                return string.Empty;

            var sizes = new List<string>{ "B", "KB", "MB", "GB", "TB" };
            double len = byteSize.Value;
            var order = 0;
            while (len >= 1024 && order < sizes.Count - 1) {
                order++;
                len /= 1024;
            }

            return $"{len:0.##} {sizes[order]}";
        }
    }
}
