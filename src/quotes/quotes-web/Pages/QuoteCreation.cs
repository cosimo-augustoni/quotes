using System.ComponentModel.DataAnnotations;

namespace quotes_web.Pages
{
    public class QuoteCreation
    {
        [Required]
        [StringLength(1024,
        ErrorMessage = "Text zu lang")]
        public string? Text { get; set; }
        [Required]
        public Guid? AuthorId { get; set; }
    }
}
