namespace KaizenBlitz.Core.Models.Enums;

/// <summary>
/// Represents the current status of a Kaizen Blitz project
/// </summary>
public enum ProjectStatus
{
    /// <summary>
    /// Project is currently being worked on
    /// </summary>
    InProgress = 0,
    
    /// <summary>
    /// Project has been successfully completed
    /// </summary>
    Completed = 1,
    
    /// <summary>
    /// Project is temporarily on hold
    /// </summary>
    OnHold = 2,
    
    /// <summary>
    /// Project has been cancelled
    /// </summary>
    Cancelled = 3
}
