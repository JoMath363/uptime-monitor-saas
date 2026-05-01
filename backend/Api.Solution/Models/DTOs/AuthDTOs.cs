using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models.DTOs
{
    public class AuthRequest
    {
        [EmailAddress]
        [Required(ErrorMessage = "{0} is required.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string Password { get; set; }
    }
}
