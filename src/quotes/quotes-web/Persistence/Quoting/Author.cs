namespace quotes_web.Persistence.Quoting;

public class Author
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }

    public required Guid FileId { get; init; }
    public  virtual File? File { get; set; }
    public virtual List<Quote> Quotes { get; set; } = new List<Quote>();
}
