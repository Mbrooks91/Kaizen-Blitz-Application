using System.ComponentModel.DataAnnotations;

namespace KaizenBlitz.Core.Models;

/// <summary>
/// Represents a Five Whys analysis tool for root cause identification
/// </summary>
public class FiveWhys
{
    /// <summary>
    /// Unique identifier for the Five Whys analysis
    /// </summary>
    [Key]
    public Guid FiveWhysId { get; set; } = Guid.NewGuid();
    
    /// <summary>
    /// Foreign key to the associated project
    /// </summary>
    [Required]
    public Guid ProjectId { get; set; }
    
    /// <summary>
    /// The initial problem statement
    /// </summary>
    [Required]
    [StringLength(1000)]
    public string ProblemStatement { get; set; } = string.Empty;
    
    /// <summary>
    /// First "Why" question answer
    /// </summary>
    [StringLength(500)]
    public string Why1 { get; set; } = string.Empty;
    
    /// <summary>
    /// Second "Why" question answer
    /// </summary>
    [StringLength(500)]
    public string Why2 { get; set; } = string.Empty;
    
    /// <summary>
    /// Third "Why" question answer
    /// </summary>
    [StringLength(500)]
    public string Why3 { get; set; } = string.Empty;
    
    /// <summary>
    /// Fourth "Why" question answer
    /// </summary>
    [StringLength(500)]
    public string Why4 { get; set; } = string.Empty;
    
    /// <summary>
    /// Fifth "Why" question answer
    /// </summary>
    [StringLength(500)]
    public string Why5 { get; set; } = string.Empty;
    
    /// <summary>
    /// Additional "Why" questions beyond the standard five (JSON array)
    /// </summary>
    public string AdditionalWhys { get; set; } = "[]";
    
    /// <summary>
    /// The identified root cause
    /// </summary>
    [StringLength(1000)]
    public string RootCause { get; set; } = string.Empty;
    
    /// <summary>
    /// Whether the analysis is complete
    /// </summary>
    public bool IsCompleted { get; set; } = false;
    
    /// <summary>
    /// Date the analysis was created
    /// </summary>
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    
    /// <summary>
    /// Date the analysis was last modified
    /// </summary>
    public DateTime ModifiedDate { get; set; } = DateTime.Now;
    
    // Navigation Property
    
    /// <summary>
    /// Associated project
    /// </summary>
    public Project? Project { get; set; }
}
