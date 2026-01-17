namespace KaizenBlitz.Core.Models.Enums;

/// <summary>
/// Represents the status of an action plan task
/// </summary>
public enum TaskStatus
{
    /// <summary>
    /// Task has not been started yet
    /// </summary>
    NotStarted = 0,
    
    /// <summary>
    /// Task is currently in progress
    /// </summary>
    InProgress = 1,
    
    /// <summary>
    /// Task has been completed
    /// </summary>
    Completed = 2,
    
    /// <summary>
    /// Task is blocked by external factors
    /// </summary>
    Blocked = 3,
    
    /// <summary>
    /// Task has been cancelled
    /// </summary>
    Cancelled = 4
}
