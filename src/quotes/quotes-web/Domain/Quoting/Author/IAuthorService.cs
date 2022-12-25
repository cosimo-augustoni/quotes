using Microsoft.AspNetCore.Components.Forms;

namespace quotes_web.Domain.Quoting.Author
{
    public interface IAuthorService
    {
        /// <returns>If Successful</returns>
        Task<bool> AddAuthorAsync(AuthorCreation authorCreation);

        Task DeleteAuthorAsync(Guid authorId);

        Task UpdateAuthorImageAsync(Guid authorId, IBrowserFile? fileEntry);
    }
}