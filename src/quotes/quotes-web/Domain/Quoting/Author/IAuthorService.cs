namespace quotes_web.Domain.Quoting.Author
{
    public interface IAuthorService
    {
        Task AddAuthorAsync(AuthorCreation authorCreation);
        Task DeleteAuthorAsync(Guid authorId);
    }
}