namespace quotes_web.Domain.Quoting.Author
{
    public class AuthorCreation
    {
        public string? Name { get; set; }

        public FileCreation FileCreation { get; set; } = new FileCreation();

        public bool Validate(out List<ValidationError> validationErrors)
        {
            var isValid = true;
            validationErrors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(this.Name))
            {
                validationErrors.Add(new ValidationError("Name muss ausgefüllt sein!", "Name"));
                isValid = false;
            }

            isValid = this.FileCreation.Validate(out var fileValidationErrors) && isValid;
            validationErrors.AddRange(fileValidationErrors);

            return isValid;
        }
    }
}
