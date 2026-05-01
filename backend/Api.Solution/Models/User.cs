using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "{0} is required.")]
        [EmailAddress]
        public required string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string PasswordHash { get; set; }

        public Address? Address { get; set; }
        public List<Project> Projects { get; set; } = new();

    }
}
