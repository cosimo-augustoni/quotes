using Imageflow.Fluent;
using Microsoft.AspNetCore.Components.Forms;

namespace quotes_web.Domain.Quoting.Author
{
    internal class ImageFileService : IImageFileService
    {
        public async Task CreateImageFileAsync(string filePath, IBrowserFile file)
        {
            await using var fileStream = System.IO.File.Create(filePath);
            using var job = new ImageJob();
            await job.BuildCommandString(
                    new StreamSource(file.OpenReadStream(file.Size), false),
                    new StreamDestination(fileStream, false),
                    "width=1200&height=1200&mode=max&scale=down")
                .Finish().InProcessAsync();
        }

        public async Task UpdateImageFileAsync(string filePath, IBrowserFile file)
        {
            File.Delete(filePath);
            await this.CreateImageFileAsync(filePath, file);
        }

        public FileCreation? GetFileCreationFromBrowserFile(IBrowserFile? file)
        {
            if (file == null)
                return null;

            var fileCreation = new FileCreation
            {
                Name = file.Name,
                FileType = file.ContentType,
                FileEntry = file
            };
            return fileCreation;
        }
    }
}
