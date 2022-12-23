using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Author;

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

        private AuthorCreation authorCreation = new AuthorCreation();

        private async Task CreateAuthor()
        {
            if (await this.AuthorService.AddAuthorAsync(this.authorCreation))
                this.NavigationManager.NavigateTo("/");
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
