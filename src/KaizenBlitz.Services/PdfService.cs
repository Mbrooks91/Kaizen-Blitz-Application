using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Text.Json;

namespace KaizenBlitz.Services;

/// <summary>
/// Service for generating PDF documents using QuestPDF
/// </summary>
public class PdfService : IPdfService
{
    public PdfService()
    {
        // Configure QuestPDF license
        QuestPDF.Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GenerateProjectSummaryAsync(Project project)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text($"Kaizen Blitz Project Summary: {project.Name}")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            // Project Details
                            column.Item().Text("Project Details").FontSize(16).Bold();
                            column.Item().Text($"Description: {project.Description}");
                            column.Item().Text($"Target Area: {project.TargetArea}");
                            column.Item().Text($"Status: {project.Status}");
                            column.Item().Text($"Progress: {project.ProgressPercentage}%");
                            column.Item().Text($"Start Date: {project.StartDate:yyyy-MM-dd}");
                            if (project.ExpectedCompletionDate.HasValue)
                                column.Item().Text($"Expected Completion: {project.ExpectedCompletionDate:yyyy-MM-dd}");

                            // Team Members
                            column.Item().PaddingTop(10).Text("Team Members").FontSize(14).Bold();
                            var teamMembers = JsonSerializer.Deserialize<List<string>>(project.TeamMembers) ?? new List<string>();
                            foreach (var member in teamMembers)
                            {
                                column.Item().Text($"• {member}");
                            }

                            // Five Whys
                            if (project.FiveWhys != null && project.FiveWhys.IsCompleted)
                            {
                                column.Item().PaddingTop(10).Text("Five Whys Analysis").FontSize(14).Bold();
                                column.Item().Text($"Problem: {project.FiveWhys.ProblemStatement}");
                                column.Item().Text($"Why 1: {project.FiveWhys.Why1}");
                                column.Item().Text($"Why 2: {project.FiveWhys.Why2}");
                                column.Item().Text($"Why 3: {project.FiveWhys.Why3}");
                                column.Item().Text($"Why 4: {project.FiveWhys.Why4}");
                                column.Item().Text($"Why 5: {project.FiveWhys.Why5}");
                                column.Item().Text($"Root Cause: {project.FiveWhys.RootCause}").Bold();
                            }

                            // Action Plan
                            if (project.ActionPlan != null && project.ActionPlan.Tasks.Any())
                            {
                                column.Item().PaddingTop(10).Text("Action Plan").FontSize(14).Bold();
                                foreach (var task in project.ActionPlan.Tasks)
                                {
                                    column.Item().Text($"• {task.TaskDescription} ({task.Status}) - {task.ResponsiblePerson}");
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                            x.Span(" of ");
                            x.TotalPages();
                        });
                });
            });

            return document.GeneratePdf();
        });
    }

    public async Task<byte[]> GenerateFiveWhysPdfAsync(FiveWhys fiveWhys)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(12));

                    page.Header()
                        .Text("Five Whys Analysis")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(15);

                            column.Item().Text("Problem Statement").FontSize(14).Bold();
                            column.Item().Text(fiveWhys.ProblemStatement);

                            column.Item().Text("Why 1").FontSize(12).Bold();
                            column.Item().Text(fiveWhys.Why1);

                            column.Item().Text("Why 2").FontSize(12).Bold();
                            column.Item().Text(fiveWhys.Why2);

                            column.Item().Text("Why 3").FontSize(12).Bold();
                            column.Item().Text(fiveWhys.Why3);

                            column.Item().Text("Why 4").FontSize(12).Bold();
                            column.Item().Text(fiveWhys.Why4);

                            column.Item().Text("Why 5").FontSize(12).Bold();
                            column.Item().Text(fiveWhys.Why5);

                            var additionalWhys = JsonSerializer.Deserialize<List<string>>(fiveWhys.AdditionalWhys) ?? new List<string>();
                            if (additionalWhys.Any())
                            {
                                for (int i = 0; i < additionalWhys.Count; i++)
                                {
                                    column.Item().Text($"Why {6 + i}").FontSize(12).Bold();
                                    column.Item().Text(additionalWhys[i]);
                                }
                            }

                            column.Item().PaddingTop(10).Text("Root Cause").FontSize(14).Bold();
                            column.Item().Text(fiveWhys.RootCause).FontColor(Colors.Red.Darken1);
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
                        });
                });
            });

            return document.GeneratePdf();
        });
    }

    public async Task<byte[]> GenerateIshikawaPdfAsync(IshikawaDiagram diagram)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4.Landscape());
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text("Ishikawa (Fishbone) Diagram")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Text("Problem Statement").FontSize(14).Bold();
                            column.Item().Text(diagram.ProblemStatement).FontColor(Colors.Red.Darken1);

                            column.Item().PaddingTop(10).Text("Categories and Causes").FontSize(14).Bold();

                            foreach (var category in diagram.Categories)
                            {
                                column.Item().PaddingTop(8).Text(category.Name).FontSize(12).Bold().FontColor(Colors.Blue.Medium);
                                var causes = JsonSerializer.Deserialize<List<string>>(category.Causes) ?? new List<string>();
                                foreach (var cause in causes)
                                {
                                    column.Item().Text($"  • {cause}");
                                }
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
                        });
                });
            });

            return document.GeneratePdf();
        });
    }

    public async Task<byte[]> GenerateActionPlanPdfAsync(ActionPlan actionPlan)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(10));

                    page.Header()
                        .Text("Action Plan")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            // Table header
                            column.Item().Row(row =>
                            {
                                row.RelativeItem().Text("Task").Bold();
                                row.RelativeItem().Text("Responsible").Bold();
                                row.RelativeItem().Text("Deadline").Bold();
                                row.RelativeItem().Text("Status").Bold();
                                row.RelativeItem().Text("Priority").Bold();
                            });

                            // Table rows
                            foreach (var task in actionPlan.Tasks)
                            {
                                column.Item().Row(row =>
                                {
                                    row.RelativeItem().Text(task.TaskDescription);
                                    row.RelativeItem().Text(task.ResponsiblePerson);
                                    row.RelativeItem().Text(task.Deadline?.ToString("yyyy-MM-dd") ?? "N/A");
                                    row.RelativeItem().Text(task.Status.ToString());
                                    row.RelativeItem().Text(task.Priority);
                                });
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
                        });
                });
            });

            return document.GeneratePdf();
        });
    }

    public async Task<byte[]> GenerateParetoChartPdfAsync(ParetoChart paretoChart)
    {
        return await Task.Run(() =>
        {
            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text($"Pareto Chart: {paretoChart.Title}")
                        .FontSize(20)
                        .Bold()
                        .FontColor(Colors.Blue.Darken2);

                    page.Content()
                        .PaddingVertical(1, Unit.Centimetre)
                        .Column(column =>
                        {
                            column.Spacing(10);

                            column.Item().Text(paretoChart.Description);

                            column.Item().PaddingTop(10).Text("Data").FontSize(14).Bold();

                            var orderedItems = paretoChart.Items.OrderByDescending(i => i.Frequency).ToList();
                            var total = orderedItems.Sum(i => i.Frequency);

                            foreach (var item in orderedItems)
                            {
                                var percentage = total > 0 ? (item.Frequency * 100.0 / total) : 0;
                                column.Item().Text($"{item.Category}: {item.Frequency} ({percentage:F1}%)");
                            }
                        });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Generated on ");
                            x.Span(DateTime.Now.ToString("yyyy-MM-dd"));
                        });
                });
            });

            return document.GeneratePdf();
        });
    }
}
