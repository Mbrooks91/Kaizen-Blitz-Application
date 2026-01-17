using System.ComponentModel.DataAnnotations;
using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents a Kaizen Blitz project
/// </summary>
public class Project
{
    /// <summary>
    /// Unique identifier for the project
    /// </summary>
    [Key]
    public Guid ProjectId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Name of the project
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// Detailed description of the project
    /// </summary>
    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Area or department targeted for improvement
    /// </summary>
    [StringLength(200)]
    public string TargetArea { get; set; } = string.Empty;
    
    /// <summary>
    /// Project start date
    /// </summary>
    [Required]
    public DateTime StartDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Expected completion date
    /// </summary>
    public DateTime? ExpectedCompletionDate { get; set; }
    
    /// <summary>
    /// Actual completion date
    /// </summary>
    public DateTime? ActualCompletionDate { get; set; }
    
    /// <summary>
    /// Current status of the project
    /// </summary>
    public ProjectStatus Status { get; set; } = ProjectStatus.InProgress;
    
    /// <summary>
    /// Progress percentage (0-100)
    /// </summary>
    [Range(0, 100)]
    public int ProgressPercentage { get; set; } = 0;
    
    /// <summary>
    /// JSON serialized list of team members
    /// </summary>
    public string TeamMembers { get; set; } = "[]";
    
    /// <summary>
    /// Date the project was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date the project was last modified
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Current phase of the Kaizen project
    /// </summary>
    public KaizenPhase CurrentPhase { get; set; } = KaizenPhase.Preparation;
    
    // Navigation Properties
    
    /// <summary>
    /// Five Whys analysis for this project
    /// </summary>
    public FiveWhys? FiveWhys { get; set; }
    
    /// <summary>
    /// Ishikawa diagram for this project
    /// </summary>
    public IshikawaDiagram? IshikawaDiagram { get; set; }
    
    /// <summary>
    /// Action plan for this project
    /// </summary>
    public ActionPlan? ActionPlan { get; set; }
    
    /// <summary>
    /// Pareto chart for this project
    /// </summary>
    public ParetoChart? ParetoChart { get; set; }
}
