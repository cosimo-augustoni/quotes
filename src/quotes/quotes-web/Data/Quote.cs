namespace quotes_web.Data;

public class Quote
{
    public required Guid Id { get; init; }

    public required string Text { get; init; }

    public required DateTime DateOfQuote { get; set; }

    public virtual required Author Author { get; init; }

    public DateTime? DeletedAt { get; set; }

}