using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "{0} is required.")]
        public required string Street { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required int Number { get; set; }

        public string? Complement { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string City { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string Neighborhood { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string State { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string ZipCode { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required string Country { get; set; }
    }
}
 
