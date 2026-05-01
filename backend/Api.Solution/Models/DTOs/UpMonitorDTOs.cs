using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models.DTOs
{
    // Requests
    public class CreateUpMonitorRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Url]
        public required string Url { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required UpMonitorState State { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required Guid ProjectId { get; set; }
    }

    public class UpdateUpMonitorRequest
    {
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public string? Title { get; set; }

        [Url]
        public string? Url { get; set; }

        public UpMonitorState? State { get; set; }
    }

    // Responses
    public class UpMonitorResponse
    {
        public required string Title { get; set; }
        public required string Url { get; set; }
        public required UpMonitorState State { get; set; }
        public required UpMonitorStatus Status { get; set; }
    }
}
