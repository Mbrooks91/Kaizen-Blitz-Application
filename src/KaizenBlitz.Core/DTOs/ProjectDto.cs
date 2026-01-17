using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Core.DTOs;

/// <summary>
/// Data transfer object for Project
/// </summary>
public class ProjectDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string TargetArea { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime? ExpectedCompletionDate { get; set; }
    public DateTime? ActualCompletionDate { get; set; }
    public ProjectStatus Status { get; set; }
    public int ProgressPercentage { get; set; }
    public List<string> TeamMembers { get; set; } = new();
    public KaizenPhase CurrentPhase { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}
