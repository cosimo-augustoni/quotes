using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using quotes_web.Domain.Quoting.Author;
using quotes_web.View.Shared;
using File = quotes_web.Persistence.Quoting.File;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorList
    {
        [Inject] 
        private IAuthorReadOnlyService AuthorReadOnlyService { get; set; } = default!;

        [Inject] 
        private IAuthorService AuthorService { get; set; } = default!;

        [Inject]
        private IImageFileService ImageFileService { get; set; } = default!;

        [Inject]
        private IDialogService DialogService { get; set; } = default!;

        private ICollection<Persistence.Quoting.Author> Authors { get; set; } = new List<Persistence.Quoting.Author>();

        protected override async Task OnInitializedAsync()
        {
            await this.LoadAuthorsAsync();
            await base.OnInitializedAsync();
        }

        private async Task LoadAuthorsAsync()
        {
            var authors = await this.AuthorReadOnlyService.GetAuthorsAsync();
            this.Authors = authors.OrderBy(a => a.Name).ToList();
            this.StateHasChanged();
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }

        private async Task OnChangedAsync(IBrowserFile e, Guid authorId)
        {
            var fileCreation = this.ImageFileService.GetFileCreationFromBrowserFile(e);
            if (fileCreation == null)
                return;

            await this.AuthorService.UpdateAuthorImageAsync(authorId, fileCreation.FileEntry);
        }
        private async Task ConfirmDelete(Guid authorId)
        {
            var dialog = await this.DialogService.ShowAsync<ConfirmDeleteDialog>("Löschen");
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await this.AuthorService.DeleteAuthorAsync(authorId);
                await this.LoadAuthorsAsync();
            }
        }
    }
}
