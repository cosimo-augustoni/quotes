using System.ComponentModel.DataAnnotations;

namespace quotes_web.Data
{
    public class AuthorCreation
    {
        [Required]
        [StringLength(255, ErrorMessage = "Name zu lang")]
        public string? Name { get; set; }
    }
}
