using Blazorise;
using Imageflow.Fluent;

namespace quotes_web.Domain.Quoting.Author
{
    internal class ImageFileService : IImageFileService
    {
        public async Task CreateImageFileAsync(string filePath, IFileEntry fileEntry)
        {
            await using var fileStream = System.IO.File.Create(filePath);
            using var job = new ImageJob();
            await job.BuildCommandString(
                    new StreamSource(fileEntry.OpenReadStream(fileEntry.Size), false),
                    new StreamDestination(fileStream, false),
                    "width=600&height=600&mode=max&scale=down")
                .Finish().InProcessAsync();
        }

        public async Task UpdateImageFileAsync(string filePath, IFileEntry fileEntry)
        {
            File.Delete(filePath);
            await this.CreateImageFileAsync(filePath, fileEntry);
        }

        public FileCreation? GetFileCreationFromEvent(FileChangedEventArgs e)
        {
            var file = e.Files.FirstOrDefault();
            if (file == null)
                return null;

            if (file.Size > 1024 * 1024 * 5)
                return null;

            var fileCreation = new FileCreation
            {
                Name = file.Name,
                FileType = file.Type,
                FileEntry = file
            };
            return fileCreation;
        }
    }
}
