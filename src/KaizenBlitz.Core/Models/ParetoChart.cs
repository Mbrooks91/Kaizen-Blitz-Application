using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents a Pareto chart for identifying vital few causes
/// </summary>
public class ParetoChart
{
    /// <summary>
    /// Unique identifier for the Pareto chart
    /// </summary>
    [Key]
    public Guid ParetoChartId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated project
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }
    
    /// <summary>
    /// Title of the Pareto analysis
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;
    
    /// <summary>
    /// Description of what is being analyzed
    /// </summary>
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the chart is complete
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date the chart was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date the chart was last modified
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    
    // Navigation Properties
    
    /// <summary>
    /// Associated project
    /// </summary>
    public Project? Project { get; set; }
    
    /// <summary>
    /// Items in the Pareto chart
    /// </summary>
    public ICollection<ParetoItem> Items { get; set; } = new List<ParetoItem>();
}
