using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents an item in a Pareto chart
/// </summary>
public class ParetoItem
{
    /// <summary>
    /// Unique identifier for the item
    /// </summary>
    [Key]
    public Guid ParetoItemId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated Pareto chart
    /// </summary>
    [Required]
    public Guid ParetoChartId { get; set; }
    
    /// <summary>
    /// Category or cause name
    /// </summary>
    [Required]
    [StringLength(200)]
    public string Category { get; set; } = string.Empty;
    
    /// <summary>
    /// Frequency or count of occurrences
    /// </summary>
    [Required]
    public int Frequency { get; set; }
    
    /// <summary>
    /// Order for display
    /// </summary>
    public int DisplayOrder { get; set; }
    
    // Navigation Property
    
    /// <summary>
    /// Associated Pareto chart
    /// </summary>
    public ParetoChart? ParetoChart { get; set; }
}
