using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models
{
    public class Post
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "{0} is required.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string Content { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required Guid UserId { get; set; }
        public required User User { get; set; }

        //[Required(ErrorMessage = "{0} is required.")]
        //public List<Comentary>? Comentaries { get; set; }
    }
}
