using System.ComponentModel.DataAnnotations;
using TaskStatus = KaizenBlitz.Core.Models.Enums.TaskStatus;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents a task in an action plan
/// </summary>
public class ActionPlanTask
{
    /// <summary>
    /// Unique identifier for the task
    /// </summary>
    [Key]
    public Guid TaskId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated action plan
    /// </summary>
    [Required]
    public Guid ActionPlanId { get; set; }
    
    /// <summary>
    /// Description of the task
    /// </summary>
    [Required]
    [StringLength(500)]
    public string TaskDescription { get; set; } = string.Empty;
    
    /// <summary>
    /// Person responsible for completing the task
    /// </summary>
    [StringLength(200)]
    public string ResponsiblePerson { get; set; } = string.Empty;
    
    /// <summary>
    /// Deadline for task completion
    /// </summary>
    public DateTime? Deadline { get; set; }
    
    /// <summary>
    /// Current status of the task
    /// </summary>
    public TaskStatus Status { get; set; } = TaskStatus.NotStarted;
    
    /// <summary>
    /// Priority level (High, Medium, Low)
    /// </summary>
    [StringLength(50)]
    public string Priority { get; set; } = "Medium";
    
    /// <summary>
    /// Additional notes about the task
    /// </summary>
    [StringLength(1000)]
    public string Notes { get; set; } = string.Empty;
    
    /// <summary>
    /// Date the task was completed
    /// </summary>
    public DateTime? CompletedDate { get; set; }
    
    // Navigation Property
    
    /// <summary>
    /// Associated action plan
    /// </summary>
    public ActionPlan? ActionPlan { get; set; }
}
