using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Author;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorList
    {
        [Inject] 
        private IAuthorReadOnlyService AuthorReadOnlyService { get; set; } = default!;

        [Inject] 
        private IAuthorService AuthorService { get; set; } = default!;

        private IReadOnlyCollection<Persistence.Quoting.Author> Authors { get; set; } = new List<Persistence.Quoting.Author>();

        [Parameter]
        public bool ShowDelete { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await this.LoadAuthorsAsync();
            await base.OnInitializedAsync();
        }

        private async Task LoadAuthorsAsync()
        {
            this.Authors = await this.AuthorReadOnlyService.GetAuthorsAsync();
        }

        private async Task DeleteAuthorAsync(Guid id)
        {
            await this.AuthorService.DeleteAuthorAsync(id);
            await this.LoadAuthorsAsync();
        }
    }
}
