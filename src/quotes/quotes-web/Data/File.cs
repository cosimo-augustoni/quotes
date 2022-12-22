namespace quotes_web.Data;

public class File
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
    public required string Base64Data { get; init; }
    public required string FileType { get; init; }

}
