using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Core.Interfaces;

/// <summary>
/// Service interface for exporting projects to various formats
/// </summary>
public interface IExportService
{
    /// <summary>
    /// Export project to Word document
    /// </summary>
    Task<byte[]> ExportToWordAsync(Project project);
    
    /// <summary>
    /// Export action plan to Excel
    /// </summary>
    Task<byte[]> ExportActionPlanToExcelAsync(ActionPlan actionPlan);
    
    /// <summary>
    /// Export Pareto data to Excel with chart
    /// </summary>
    Task<byte[]> ExportParetoDataToExcelAsync(ParetoChart paretoChart);
}
