using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents an Ishikawa (Fishbone) diagram for cause analysis
/// </summary>
public class IshikawaDiagram
{
    /// <summary>
    /// Unique identifier for the Ishikawa diagram
    /// </summary>
    [Key]
    public Guid IshikawaId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated project
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }
    
    /// <summary>
    /// The problem statement being analyzed
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string ProblemStatement { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the diagram is complete
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date the diagram was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date the diagram was last modified
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    
    // Navigation Properties
    
    /// <summary>
    /// Associated project
    /// </summary>
    public Project? Project { get; set; }
    
    /// <summary>
    /// Categories of causes in the diagram
    /// </summary>
    public ICollection<IshikawaCategory> Categories { get; set; } = new List<IshikawaCategory>();
}
