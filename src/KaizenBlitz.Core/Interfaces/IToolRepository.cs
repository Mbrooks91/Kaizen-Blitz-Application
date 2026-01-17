using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Core.Interfaces;

/// <summary>
/// Repository interface for Kaizen tools operations
/// </summary>
public interface IToolRepository
{
    /// <summary>
    /// Get Five Whys by project ID
    /// </summary>
    Task<FiveWhys?> GetFiveWhysByProjectIdAsync(Guid projectId);
    
    /// <summary>
    /// Save or update Five Whys
    /// </summary>
    Task<FiveWhys> SaveFiveWhysAsync(FiveWhys fiveWhys);
    
    /// <summary>
    /// Get Ishikawa diagram by project ID
    /// </summary>
    Task<IshikawaDiagram?> GetIshikawaByProjectIdAsync(Guid projectId);
    
    /// <summary>
    /// Save or update Ishikawa diagram
    /// </summary>
    Task<IshikawaDiagram> SaveIshikawaAsync(IshikawaDiagram ishikawa);
    
    /// <summary>
    /// Get Action Plan by project ID
    /// </summary>
    Task<ActionPlan?> GetActionPlanByProjectIdAsync(Guid projectId);
    
    /// <summary>
    /// Save or update Action Plan
    /// </summary>
    Task<ActionPlan> SaveActionPlanAsync(ActionPlan actionPlan);
    
    /// <summary>
    /// Get Pareto Chart by project ID
    /// </summary>
    Task<ParetoChart?> GetParetoChartByProjectIdAsync(Guid projectId);
    
    /// <summary>
    /// Save or update Pareto Chart
    /// </summary>
    Task<ParetoChart> SaveParetoChartAsync(ParetoChart paretoChart);
}
