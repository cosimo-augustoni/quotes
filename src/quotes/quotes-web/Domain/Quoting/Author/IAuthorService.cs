using Blazorise;

namespace quotes_web.Domain.Quoting.Author
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AuthorCreation authorCreation);
        Task DeleteAuthorAsync(Guid authorId);
        Task UpdateAuthorImageAsync(Guid authorId, IFileEntry fileEntry);
    }
}