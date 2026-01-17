using Microsoft.EntityFrameworkCore;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Data.Repositories;

/// <summary>
/// Repository implementation for Kaizen tools operations
/// </summary>
public class ToolRepository : IToolRepository
{
    private readonly ApplicationDbContext _context;

    public ToolRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Five Whys operations
    public async Task<FiveWhys?> GetFiveWhysByProjectIdAsync(Guid projectId)
    {
        return await _context.FiveWhys
            .FirstOrDefaultAsync(f => f.ProjectId == projectId);
    }

    public async Task<FiveWhys> SaveFiveWhysAsync(FiveWhys fiveWhys)
    {
        fiveWhys.ModifiedDate = DateTime.Now;
        
        var existing = await _context.FiveWhys
            .FirstOrDefaultAsync(f => f.FiveWhysId == fiveWhys.FiveWhysId);

        if (existing == null)
        {
            fiveWhys.CreatedDate = DateTime.Now;
            _context.FiveWhys.Add(fiveWhys);
        }
        else
        {
            _context.Entry(existing).CurrentValues.SetValues(fiveWhys);
        }

        await _context.SaveChangesAsync();
        return fiveWhys;
    }

    // Ishikawa operations
    public async Task<IshikawaDiagram?> GetIshikawaByProjectIdAsync(Guid projectId)
    {
        return await _context.IshikawaDiagrams
            .Include(i => i.Categories)
            .FirstOrDefaultAsync(i => i.ProjectId == projectId);
    }

    public async Task<IshikawaDiagram> SaveIshikawaAsync(IshikawaDiagram ishikawa)
    {
        ishikawa.ModifiedDate = DateTime.Now;
        
        var existing = await _context.IshikawaDiagrams
            .Include(i => i.Categories)
            .FirstOrDefaultAsync(i => i.IshikawaId == ishikawa.IshikawaId);

        if (existing == null)
        {
            ishikawa.CreatedDate = DateTime.Now;
            _context.IshikawaDiagrams.Add(ishikawa);
        }
        else
        {
            // Update main entity
            _context.Entry(existing).CurrentValues.SetValues(ishikawa);
            
            // Update categories
            existing.Categories.Clear();
            foreach (var category in ishikawa.Categories)
            {
                existing.Categories.Add(category);
            }
        }

        await _context.SaveChangesAsync();
        return ishikawa;
    }

    // Action Plan operations
    public async Task<ActionPlan?> GetActionPlanByProjectIdAsync(Guid projectId)
    {
        return await _context.ActionPlans
            .Include(a => a.Tasks)
            .FirstOrDefaultAsync(a => a.ProjectId == projectId);
    }

    public async Task<ActionPlan> SaveActionPlanAsync(ActionPlan actionPlan)
    {
        actionPlan.ModifiedDate = DateTime.Now;
        
        var existing = await _context.ActionPlans
            .Include(a => a.Tasks)
            .FirstOrDefaultAsync(a => a.ActionPlanId == actionPlan.ActionPlanId);

        if (existing == null)
        {
            actionPlan.CreatedDate = DateTime.Now;
            _context.ActionPlans.Add(actionPlan);
        }
        else
        {
            // Update main entity
            _context.Entry(existing).CurrentValues.SetValues(actionPlan);
            
            // Update tasks
            existing.Tasks.Clear();
            foreach (var task in actionPlan.Tasks)
            {
                existing.Tasks.Add(task);
            }
        }

        await _context.SaveChangesAsync();
        return actionPlan;
    }

    // Pareto Chart operations
    public async Task<ParetoChart?> GetParetoChartByProjectIdAsync(Guid projectId)
    {
        return await _context.ParetoCharts
            .Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.ProjectId == projectId);
    }

    public async Task<ParetoChart> SaveParetoChartAsync(ParetoChart paretoChart)
    {
        paretoChart.ModifiedDate = DateTime.Now;
        
        var existing = await _context.ParetoCharts
            .Include(p => p.Items)
            .FirstOrDefaultAsync(p => p.ParetoChartId == paretoChart.ParetoChartId);

        if (existing == null)
        {
            paretoChart.CreatedDate = DateTime.Now;
            _context.ParetoCharts.Add(paretoChart);
        }
        else
        {
            // Update main entity
            _context.Entry(existing).CurrentValues.SetValues(paretoChart);
            
            // Update items
            existing.Items.Clear();
            foreach (var item in paretoChart.Items)
            {
                existing.Items.Add(item);
            }
        }

        await _context.SaveChangesAsync();
        return paretoChart;
    }
}
