using Blazorise;

namespace quotes_web.Domain.Quoting.Author;

public class FileCreation
{
    public string? Name { get; init; }
    public IFileEntry? FileEntry { get; set; }
    public string? FileType { get; init; }

    public bool Validate(out List<ValidationError> validationErrors)
    {
        var isValid = true;
        validationErrors = new List<ValidationError>();
        if (string.IsNullOrWhiteSpace(this.Name) || this.FileEntry == null || string.IsNullOrWhiteSpace(this.FileType))
        {
            validationErrors.Add(new ValidationError("Bild ungültig!", "Bild"));
            return false;
        }

        if (this.FileType != "image/png" && this.FileType != "image/jpg" && this.FileType != "image/gif")
        {
            validationErrors.Add(new ValidationError("Nur Bilder von typ PNG, JPG, und GIF sind erlaubt!", "Bild"));
            isValid = false;
        }

        if (this.FileEntry.Size == 0)
        {
            validationErrors.Add(new ValidationError("Bild ist korrupt!", "Bild"));
            isValid = false;
        }

        if (this.FileEntry.Size > 1024 * 1024 * 5)
        {
            validationErrors.Add(new ValidationError("Bilder dürfen Maximal 5MB gross sein!", "Bild"));
            isValid = false;
        }

        return isValid;
    }
}