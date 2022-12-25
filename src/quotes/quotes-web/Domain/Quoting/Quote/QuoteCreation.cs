namespace quotes_web.Domain.Quoting.Quote
{
    public class QuoteCreation
    {
        public string? Text { get; set; }
        public Guid? AuthorId { get; set; }
        public  DateTime? DateOfQuote { get; set; } = DateTime.Today;
        public bool Validate(out List<ValidationError> validationErrors)
        {
            var isValid = true;
            validationErrors = new List<ValidationError>();

            if (string.IsNullOrWhiteSpace(this.Text))
            {
                validationErrors.Add(new ValidationError("Zitat muss ausgefüllt sein!", "Text"));
                isValid = false;
            }

            if (this.Text?.Length > 1024)
            {
                validationErrors.Add(new ValidationError("Zitat zu lang!", "Text"));
                isValid = false;
            }
            if (AuthorId == null)
            {
                validationErrors.Add(new ValidationError("Author muss ausgewählt sein!", "Author"));
                isValid = false;
            }
            if (DateOfQuote == null)
            {
                validationErrors.Add(new ValidationError("Tag des Zitates muss ausgewählt sein!", "Tag des Zitates"));
                isValid = false;
            }
            return isValid;
        }
    }
}
