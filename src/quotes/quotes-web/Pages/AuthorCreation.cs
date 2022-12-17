using System.ComponentModel.DataAnnotations;

namespace quotes_web.Pages
{
    public class AuthorCreation
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name zu lang")]
        public string? Name { get; set; }
    }
}
