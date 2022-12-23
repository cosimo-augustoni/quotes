using Blazorise;
using Microsoft.AspNetCore.Components;
using quotes_web.Domain.Quoting.Author;

namespace quotes_web.View.Quoting.Author
{
    public partial class AuthorForm
    {
        [Inject]
        private IAuthorService AuthorService { get; set; } = default!;

        [Inject]
        private NavigationManager NavigationManager { get; set; } = default!;

        private Validations? validations;
        private AuthorCreation authorCreation = new AuthorCreation();
        private string? fileValidationError;
        private static readonly string FileIsToLarge = "Bild ist zu gross";
        private static readonly string FileIsRequired = "Bild ist erforderlich";
        private static readonly int FiveMb = 1024 * 1024 * 5;

        private async Task CreateAuthor()
        {
            if (await this.validations.ValidateAll())
            {
                await this.AuthorService.AddAuthorAsync(this.authorCreation);
                this.NavigationManager.NavigateTo("/");
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
                if (file.Size > FiveMb)
                {
                    this.fileValidationError = FileIsToLarge;
                    return;
                }

                var fileCreation = new FileCreation
                {
                    Name = file.Name,
                    FileType = file.Type,
                    FileEntry = file
                };
                this.authorCreation.FileCreation = fileCreation;

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
