using Microsoft.AspNetCore.Components;
using quotes_web.Data;

namespace quotes_web.Quoting.View.Author
{
    public partial class AuthorList
    {
        [Inject] 
        private QuotesContext QuotesContext { get; set; } = default!;

        [Parameter]
        public bool ShowDelete { get; set; }

        private void DeleteAuthor(Guid id)
        {
            var author = this.QuotesContext.Authors.Find(id);
            this.QuotesContext.Remove(author);
            this.QuotesContext.SaveChanges();
        }
    }
}
