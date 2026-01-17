using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents a category in an Ishikawa diagram (e.g., People, Process, Materials)
/// </summary>
public class IshikawaCategory
{
    /// <summary>
    /// Unique identifier for the category
    /// </summary>
    [Key]
    public Guid CategoryId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated Ishikawa diagram
    /// </summary>
    [Required]
    public Guid IshikawaId { get; set; }
    
    /// <summary>
    /// Name of the category (e.g., "People", "Process", "Materials", "Equipment", "Environment", "Management")
    /// </summary>
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    /// <summary>
    /// List of causes under this category (JSON serialized)
    /// </summary>
    public string Causes { get; set; } = "[]";
    
    // Navigation Property
    
    /// <summary>
    /// Associated Ishikawa diagram
    /// </summary>
    public IshikawaDiagram? IshikawaDiagram { get; set; }
}
