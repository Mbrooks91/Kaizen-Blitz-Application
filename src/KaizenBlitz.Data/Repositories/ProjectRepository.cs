using Microsoft.EntityFrameworkCore;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Data.Repositories;

/// <summary>
/// Repository implementation for Project operations
/// </summary>
public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.FiveWhys)
            .Include(p => p.IshikawaDiagram)
                .ThenInclude(i => i!.Categories)
            .Include(p => p.ActionPlan)
                .ThenInclude(a => a!.Tasks)
            .Include(p => p.ParetoChart)
                .ThenInclude(pc => pc!.Items)
            .OrderByDescending(p => p.ModifiedDate)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.FiveWhys)
            .Include(p => p.IshikawaDiagram)
                .ThenInclude(i => i!.Categories)
            .Include(p => p.ActionPlan)
                .ThenInclude(a => a!.Tasks)
            .Include(p => p.ParetoChart)
                .ThenInclude(pc => pc!.Items)
            .FirstOrDefaultAsync(p => p.ProjectId == id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        project.CreatedDate = DateTime.Now;
        project.ModifiedDate = DateTime.Now;
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        project.ModifiedDate = DateTime.Now;
        
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        
        return project;
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Project>> SearchAsync(string searchTerm)
    {
        var lowerSearchTerm = searchTerm.ToLower();
        
        return await _context.Projects
            .Include(p => p.FiveWhys)
            .Include(p => p.IshikawaDiagram)
            .Include(p => p.ActionPlan)
            .Include(p => p.ParetoChart)
            .Where(p => p.Name.ToLower().Contains(lowerSearchTerm) ||
                       p.Description.ToLower().Contains(lowerSearchTerm) ||
                       p.TargetArea.ToLower().Contains(lowerSearchTerm))
            .OrderByDescending(p => p.ModifiedDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetCompletedProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.FiveWhys)
            .Include(p => p.IshikawaDiagram)
            .Include(p => p.ActionPlan)
            .Include(p => p.ParetoChart)
            .Where(p => p.Status == ProjectStatus.Completed)
            .OrderByDescending(p => p.ActualCompletionDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Project>> GetInProgressProjectsAsync()
    {
        return await _context.Projects
            .Include(p => p.FiveWhys)
            .Include(p => p.IshikawaDiagram)
            .Include(p => p.ActionPlan)
            .Include(p => p.ParetoChart)
            .Where(p => p.Status == ProjectStatus.InProgress)
            .OrderByDescending(p => p.ModifiedDate)
            .ToListAsync();
    }
}
