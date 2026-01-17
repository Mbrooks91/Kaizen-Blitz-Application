using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents an action plan for implementing improvements
/// </summary>
public class ActionPlan
{
    /// <summary>
    /// Unique identifier for the action plan
    /// </summary>
    [Key]
    public Guid ActionPlanId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated project
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }
    
    /// <summary>
    /// Whether the action plan is complete
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date the action plan was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date the action plan was last modified
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    
    // Navigation Properties
    
    /// <summary>
    /// Associated project
    /// </summary>
    public Project? Project { get; set; }
    
    /// <summary>
    /// Tasks in the action plan
    /// </summary>
    public ICollection<ActionPlanTask> Tasks { get; set; } = new List<ActionPlanTask>();
}
