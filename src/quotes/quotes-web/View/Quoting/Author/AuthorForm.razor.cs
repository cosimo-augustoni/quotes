using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Data;
using File = quotes_web.Data.File;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorForm
    {
        [Inject]
        private QuotesContext QuotesContext { get; set; } = default!;

        private Validations? validations;
        private AuthorCreation authorCreation = new AuthorCreation();
        private FileCreation fileCreation = new FileCreation();
        private string? fileValidationError;
        private static readonly string FileIsToLarge = "Bild ist zu gross";
        private static readonly string FileIsRequired = "Bild ist erforderlich";
        private static readonly int TwoMB = 2000000;

        private async Task CreateAuthor()
        {
            if (await this.validations.ValidateAll())
            {
                var file = new File
                {
                    Id = Guid.NewGuid(),
                    Base64Data = this.fileCreation.Base64Data,
                    Name = this.fileCreation.Name,
                    FileType = this.fileCreation.FileType
                };
                this.QuotesContext.Add(file);
                this.QuotesContext.Add(new Data.Author
                {
                    Id = Guid.NewGuid(),
                    Name = this.authorCreation.Name,
                    FileId = file.Id
                });
                await this.QuotesContext.SaveChangesAsync();
            }
        }

        private void ValidatePhoto(ValidatorEventArgs e)
        {
            if (e.Value == null)
            {
                this.fileValidationError = FileIsRequired;
            }
            e.Status = string.IsNullOrEmpty(this.fileValidationError) ? ValidationStatus.Success : ValidationStatus.Error;
        }


        private async Task OnChanged(FileChangedEventArgs e)
        {
            try
            {
                var file = e.Files.FirstOrDefault();
                if (file == null)
                {
                    return;
                }
                if (file.Size > TwoMB)
                {
                    this.fileValidationError = FileIsToLarge;
                    return;
                }

                using var stream = new MemoryStream();
                await file.OpenReadStream(file.Size).CopyToAsync(stream);
                stream.Seek(0, SeekOrigin.Begin);
                this.fileCreation = new FileCreation
                {
                    Name = file.Name,
                    Base64Data = Convert.ToBase64String(stream.ToArray()),
                    FileType = file.Type
                };
                this.fileValidationError = "";
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
            finally
            {
                this.StateHasChanged();
            }
        }
    }
}
