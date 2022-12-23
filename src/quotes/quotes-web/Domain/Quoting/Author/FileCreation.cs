using Blazorise;

namespace quotes_web.Domain.Quoting.Author;

public class FileCreation
{
    public  string? Name { get; init; }
    public IFileEntry FileEntry { get; set; }
    public string? FileType { get; init; }
}