using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Core.DTOs;

/// <summary>
/// Summary data transfer object for Project (for list views)
/// </summary>
public class ProjectSummaryDto
{
    public Guid ProjectId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ProjectStatus Status { get; set; }
    public int ProgressPercentage { get; set; }
    public KaizenPhase CurrentPhase { get; set; }
    public DateTime ModifiedDate { get; set; }
}
