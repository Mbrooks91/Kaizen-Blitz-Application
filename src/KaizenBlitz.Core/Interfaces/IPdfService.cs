using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Core.Interfaces;

/// <summary>
/// Service interface for PDF generation
/// </summary>
public interface IPdfService
{
    /// <summary>
    /// Generate a comprehensive project summary PDF
    /// </summary>
    Task<byte[]> GenerateProjectSummaryAsync(Project project);
    
    /// <summary>
    /// Generate a Five Whys analysis PDF
    /// </summary>
    Task<byte[]> GenerateFiveWhysPdfAsync(FiveWhys fiveWhys);
    
    /// <summary>
    /// Generate an Ishikawa diagram PDF
    /// </summary>
    Task<byte[]> GenerateIshikawaPdfAsync(IshikawaDiagram diagram);
    
    /// <summary>
    /// Generate an Action Plan PDF
    /// </summary>
    Task<byte[]> GenerateActionPlanPdfAsync(ActionPlan actionPlan);
    
    /// <summary>
    /// Generate a Pareto Chart PDF
    /// </summary>
    Task<byte[]> GenerateParetoChartPdfAsync(ParetoChart paretoChart);
}
