using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Author;
using System.Runtime.CompilerServices;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorForm
    {
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
            loading = true;
            if (await this.AuthorService.AddAuthorAsync(this.authorCreation))
                this.NavigationManager.NavigateTo("/");
            loading = false;

        }

        private void OnChanged(FileChangedEventArgs e)
        {
            var fileCreation = this.ImageFileService.GetFileCreationFromEvent(e);
            if (fileCreation == null)
                return;

            this.authorCreation.FileCreation = fileCreation;
        }


    }
}
