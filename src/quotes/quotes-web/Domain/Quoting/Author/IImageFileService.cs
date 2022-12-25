using Microsoft.AspNetCore.Components.Forms;

namespace quotes_web.Domain.Quoting.Author
{
    public interface IImageFileService
    {
        Task CreateImageFileAsync(string filePath, IBrowserFile fileEntry);
        Task UpdateImageFileAsync(string filePath, IBrowserFile fileEntry);
        FileCreation? GetFileCreationFromBrowserFile(IBrowserFile file);
    }
}