namespace quotes_web.Domain.Quoting.Author
{
    public interface IAuthorReadOnlyService
    {
        Task<IReadOnlyCollection<Persistence.Quoting.Author>> GetAuthorsAsync();
    }
}