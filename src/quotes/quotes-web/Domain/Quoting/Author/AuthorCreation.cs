using System.ComponentModel.DataAnnotations;

namespace quotes_web.Domain.Quoting.Author
{
    public class AuthorCreation
    {
        [Required(ErrorMessage = "Name ist erforderlich")]
        [StringLength(255, ErrorMessage = "Name zu lang")]
        public string? Name { get; set; }

        public FileCreation FileCreation { get; set; } = new FileCreation();
    }
}
