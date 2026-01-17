using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Text.Json;

namespace KaizenBlitz.Services;

/// <summary>
/// Service for exporting projects to Word documents
/// </summary>
public class WordExportService : IExportService
{
    public async Task<byte[]> ExportToWordAsync(Project project)
    {
        return await Task.Run(() =>
        {
            using var memoryStream = new MemoryStream();
            using (var wordDocument = WordprocessingDocument.Create(memoryStream, WordprocessingDocumentType.Document))
            {
                var mainPart = wordDocument.AddMainDocumentPart();
                mainPart.Document = new Document();
                var body = mainPart.Document.AppendChild(new Body());

                // Title
                AddHeading(body, $"Kaizen Blitz Project: {project.Name}", 1);

                // Project Details
                AddHeading(body, "Project Details", 2);
                AddParagraph(body, $"Description: {project.Description}");
                AddParagraph(body, $"Target Area: {project.TargetArea}");
                AddParagraph(body, $"Status: {project.Status}");
                AddParagraph(body, $"Progress: {project.ProgressPercentage}%");
                AddParagraph(body, $"Start Date: {project.StartDate:yyyy-MM-dd}");
                if (project.ExpectedCompletionDate.HasValue)
                    AddParagraph(body, $"Expected Completion: {project.ExpectedCompletionDate:yyyy-MM-dd}");

                // Team Members
                AddHeading(body, "Team Members", 2);
                var teamMembers = JsonSerializer.Deserialize<List<string>>(project.TeamMembers) ?? new List<string>();
                foreach (var member in teamMembers)
                {
                    AddParagraph(body, $"• {member}");
                }

                // Five Whys
                if (project.FiveWhys != null && project.FiveWhys.IsCompleted)
                {
                    AddHeading(body, "Five Whys Analysis", 2);
                    AddParagraph(body, $"Problem Statement: {project.FiveWhys.ProblemStatement}");
                    AddParagraph(body, $"Why 1: {project.FiveWhys.Why1}");
                    AddParagraph(body, $"Why 2: {project.FiveWhys.Why2}");
                    AddParagraph(body, $"Why 3: {project.FiveWhys.Why3}");
                    AddParagraph(body, $"Why 4: {project.FiveWhys.Why4}");
                    AddParagraph(body, $"Why 5: {project.FiveWhys.Why5}");
                    AddParagraph(body, $"Root Cause: {project.FiveWhys.RootCause}");
                }

                // Ishikawa Diagram
                if (project.IshikawaDiagram != null && project.IshikawaDiagram.IsCompleted)
                {
                    AddHeading(body, "Ishikawa Diagram", 2);
                    AddParagraph(body, $"Problem Statement: {project.IshikawaDiagram.ProblemStatement}");
                    foreach (var category in project.IshikawaDiagram.Categories)
                    {
                        AddHeading(body, category.Name, 3);
                        var causes = JsonSerializer.Deserialize<List<string>>(category.Causes) ?? new List<string>();
                        foreach (var cause in causes)
                        {
                            AddParagraph(body, $"• {cause}");
                        }
                    }
                }

                // Action Plan
                if (project.ActionPlan != null && project.ActionPlan.Tasks.Any())
                {
                    AddHeading(body, "Action Plan", 2);
                    foreach (var task in project.ActionPlan.Tasks)
                    {
                        AddParagraph(body, $"Task: {task.TaskDescription}");
                        AddParagraph(body, $"  Responsible: {task.ResponsiblePerson}");
                        AddParagraph(body, $"  Status: {task.Status}");
                        AddParagraph(body, $"  Deadline: {task.Deadline?.ToString("yyyy-MM-dd") ?? "N/A"}");
                        AddParagraph(body, "");
                    }
                }

                mainPart.Document.Save();
            }

            return memoryStream.ToArray();
        });
    }

    public async Task<byte[]> ExportActionPlanToExcelAsync(ActionPlan actionPlan)
    {
        // Delegating to ExcelExportService
        var excelService = new ExcelExportService();
        return await excelService.ExportActionPlanToExcelAsync(actionPlan);
    }

    public async Task<byte[]> ExportParetoDataToExcelAsync(ParetoChart paretoChart)
    {
        // Delegating to ExcelExportService
        var excelService = new ExcelExportService();
        return await excelService.ExportParetoDataToExcelAsync(paretoChart);
    }

    private void AddHeading(Body body, string text, int level)
    {
        var paragraph = body.AppendChild(new Paragraph());
        var run = paragraph.AppendChild(new Run());
        var runProperties = run.AppendChild(new RunProperties());

        runProperties.AppendChild(new Bold());
        runProperties.AppendChild(new FontSize { Val = level == 1 ? "32" : level == 2 ? "24" : "20" });

        run.AppendChild(new Text(text));
    }

    private void AddParagraph(Body body, string text)
    {
        var paragraph = body.AppendChild(new Paragraph());
        var run = paragraph.AppendChild(new Run());
        run.AppendChild(new Text(text));
    }
}
