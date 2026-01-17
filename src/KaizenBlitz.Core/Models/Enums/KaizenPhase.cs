namespace KaizenBlitz.Core.Models.Enums;

/// <summary>
/// Represents the phases of a Kaizen Blitz project
/// </summary>
public enum KaizenPhase
{
    /// <summary>
    /// Initial preparation and planning phase
    /// </summary>
    Preparation = 0,
    
    /// <summary>
    /// Analysis phase - identifying root causes
    /// </summary>
    Analysis = 1,
    
    /// <summary>
    /// Improvement phase - developing solutions
    /// </summary>
    Improvement = 2,
    
    /// <summary>
    /// Implementation phase - executing action plans
    /// </summary>
    Implementation = 3,
    
    /// <summary>
    /// Review phase - evaluating results
    /// </summary>
    Review = 4
}
