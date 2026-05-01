using System.ComponentModel.DataAnnotations;

namespace Api.Solution.Models
{
    public class UpMonitor
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "{0} is required.")]
        [MaxLength(50, ErrorMessage = "{0} cannot exceed 50 characters")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        [Url]
        public required string Url { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required UpMonitorState State { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required UpMonitorStatus Status { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public required Guid ProjectId { get; set; }
        public Project Project { get; set; } = null!;
    }

    public enum UpMonitorState
    {
        Active = 1,
        Paused = 2
    }

    public enum UpMonitorStatus
    {
        Up = 1,
        Down = 2
    }
}
