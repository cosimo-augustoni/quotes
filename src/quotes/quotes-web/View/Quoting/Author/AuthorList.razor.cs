using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Author;
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

        private ICollection<Persistence.Quoting.Author> Authors { get; set; } = new List<Persistence.Quoting.Author>();

        private ConfirmationModal? modalRef;
        private Func<Task> subscription;
        protected override async Task OnInitializedAsync()
        {
            await this.LoadAuthorsAsync();
            await base.OnInitializedAsync();
        }

        private async Task LoadAuthorsAsync()
        {
            var authors = await this.AuthorReadOnlyService.GetAuthorsAsync();
            this.Authors = authors.OrderBy(a => a.Name).ToList();
            StateHasChanged();
        }

        private async Task DeleteAuthorAsync(Guid id)
        {
            await this.AuthorService.DeleteAuthorAsync(id);
            await this.LoadAuthorsAsync();
            modalRef.OnConfirm -= subscription;
        }

        private string GetImagePath(File file)
        {
            return $"/images/{file.FileName}";
        }

        private async Task OnChangedAsync(FileChangedEventArgs e, Guid authorId)
        {
            var fileCreation = this.ImageFileService.GetFileCreationFromEvent(e);
            if (fileCreation == null)
                return;

            await this.AuthorService.UpdateAuthorImageAsync(authorId, fileCreation.FileEntry);
        }
        private Task ShowModal(Guid authorId)
        {
            subscription = async () => await DeleteAuthorAsync(authorId);
            modalRef.OnConfirm += subscription;
            return modalRef.ShowModal();
        }
    }
}
