namespace quotes_web.Data;

public class Author
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public virtual List<Quote> Quotes { get; set; } = new List<Quote>();
}