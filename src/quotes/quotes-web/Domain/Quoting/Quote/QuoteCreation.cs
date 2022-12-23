using System.ComponentModel.DataAnnotations;

namespace quotes_web.Domain.Quoting.Quote
{
    public class QuoteCreation
    {
        [Required(ErrorMessage = "Zitat ist erforderlich")]
        [StringLength(1024,
        ErrorMessage = "Text zu lang")]
        public string? Text { get; set; }
        [Required(ErrorMessage = "Author ist erforderlich")]
        public Guid? AuthorId { get; set; }
        [Required(ErrorMessage = "Tag des Zitates ist erforderlich")]
        public  DateTime? DateOfQuote { get; set; }
    }
}
