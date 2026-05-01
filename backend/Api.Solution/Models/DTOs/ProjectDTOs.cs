using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models.DTOs
{
    // Requests
    public class CreateProjectRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(200, ErrorMessage = "{0} cannot exceed 200 characters")]
        public required string Description { get; set; }
    }

    public class UpdateProjectRequest
    {
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public string? Title { get; set; }

        [MaxLength(200, ErrorMessage = "{0} cannot exceed 200 characters")]
        public string? Description { get; set; }
    }

    // Responses
    public class ProjectResponse
    {
        public required string Title { get; set; }
        public required string Description { get; set; }
        public List<UpMonitor> UpMonitors { get; set; } = new List<UpMonitor> { };
    }
}
