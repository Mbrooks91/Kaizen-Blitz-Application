using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Core.Interfaces;

/// <summary>
/// Repository interface for Project operations
/// </summary>
public interface IProjectRepository
{
    /// <summary>
    /// Get all projects
    /// </summary>
    Task<IEnumerable<Project>> GetAllAsync();
    
    /// <summary>
    /// Get a project by ID with all related entities
    /// </summary>
    Task<Project?> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Create a new project
    /// </summary>
    Task<Project> CreateAsync(Project project);
    
    /// <summary>
    /// Update an existing project
    /// </summary>
    Task<Project> UpdateAsync(Project project);
    
    /// <summary>
    /// Delete a project
    /// </summary>
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Search projects by name or description
    /// </summary>
    Task<IEnumerable<Project>> SearchAsync(string searchTerm);
    
    /// <summary>
    /// Get all completed projects
    /// </summary>
    Task<IEnumerable<Project>> GetCompletedProjectsAsync();
    
    /// <summary>
    /// Get all in-progress projects
    /// </summary>
    Task<IEnumerable<Project>> GetInProgressProjectsAsync();
}
