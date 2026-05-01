using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models
{
    public class Project
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(200, ErrorMessage = "{0} cannot exceed 200 characters")]
        public required string Description { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public List<UpMonitor> UpMonitors { get; set; } = new();
    }
}
