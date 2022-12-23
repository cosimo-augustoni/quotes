namespace quotes_web.Persistence.Quoting;

public class Quote
{
    public required Guid Id { get; init; }

    public required string Text { get; init; }

    public required DateTime DateOfQuote { get; set; }

    public required Guid AuthorId { get; init; }
    public virtual Author? Author { get; init; }

    public DateTime? DeletedAt { get; set; }

}