using Blazorise;

namespace quotes_web.Domain.Quoting.Author
{
    public interface IImageFileService
    {
        Task CreateImageFileAsync(string filePath, IFileEntry fileEntry);
        Task UpdateImageFileAsync(string filePath, IFileEntry fileEntry);
        FileCreation? GetFileCreationFromEvent(FileChangedEventArgs e);
    }
}