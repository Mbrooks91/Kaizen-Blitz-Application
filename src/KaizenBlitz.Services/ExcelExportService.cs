using KaizenBlitz.Core.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace KaizenBlitz.Services;

/// <summary>
/// Service for exporting data to Excel
/// </summary>
public class ExcelExportService
{
    public ExcelExportService()
    {
        // Set EPPlus license context
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<byte[]> ExportActionPlanToExcelAsync(ActionPlan actionPlan)
    {
        return await Task.Run(() =>
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Action Plan");

            // Headers
            worksheet.Cells[1, 1].Value = "Task Description";
            worksheet.Cells[1, 2].Value = "Responsible Person";
            worksheet.Cells[1, 3].Value = "Deadline";
            worksheet.Cells[1, 4].Value = "Status";
            worksheet.Cells[1, 5].Value = "Priority";
            worksheet.Cells[1, 6].Value = "Notes";
            worksheet.Cells[1, 7].Value = "Completed Date";

            // Style headers
            using (var range = worksheet.Cells[1, 1, 1, 7])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
            }

            // Data
            int row = 2;
            foreach (var task in actionPlan.Tasks)
            {
                worksheet.Cells[row, 1].Value = task.TaskDescription;
                worksheet.Cells[row, 2].Value = task.ResponsiblePerson;
                worksheet.Cells[row, 3].Value = task.Deadline?.ToString("yyyy-MM-dd") ?? "";
                worksheet.Cells[row, 4].Value = task.Status.ToString();
                worksheet.Cells[row, 5].Value = task.Priority;
                worksheet.Cells[row, 6].Value = task.Notes;
                worksheet.Cells[row, 7].Value = task.CompletedDate?.ToString("yyyy-MM-dd") ?? "";
                row++;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            return package.GetAsByteArray();
        });
    }

    public async Task<byte[]> ExportParetoDataToExcelAsync(ParetoChart paretoChart)
    {
        return await Task.Run(() =>
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Pareto Data");

            // Title
            worksheet.Cells[1, 1].Value = paretoChart.Title;
            worksheet.Cells[1, 1].Style.Font.Bold = true;
            worksheet.Cells[1, 1].Style.Font.Size = 14;

            // Headers
            worksheet.Cells[3, 1].Value = "Category";
            worksheet.Cells[3, 2].Value = "Frequency";
            worksheet.Cells[3, 3].Value = "Percentage";
            worksheet.Cells[3, 4].Value = "Cumulative %";

            // Style headers
            using (var range = worksheet.Cells[3, 1, 3, 4])
            {
                range.Style.Font.Bold = true;
                range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGreen);
            }

            // Data
            var orderedItems = paretoChart.Items.OrderByDescending(i => i.Frequency).ToList();
            var total = orderedItems.Sum(i => i.Frequency);
            double cumulative = 0;

            int row = 4;
            foreach (var item in orderedItems)
            {
                var percentage = total > 0 ? (item.Frequency * 100.0 / total) : 0;
                cumulative += percentage;

                worksheet.Cells[row, 1].Value = item.Category;
                worksheet.Cells[row, 2].Value = item.Frequency;
                worksheet.Cells[row, 3].Value = percentage;
                worksheet.Cells[row, 3].Style.Numberformat.Format = "0.00%";
                worksheet.Cells[row, 4].Value = cumulative / 100;
                worksheet.Cells[row, 4].Style.Numberformat.Format = "0.00%";
                row++;
            }

            // Auto-fit columns
            worksheet.Cells.AutoFitColumns();

            // Add a simple chart
            var chart = worksheet.Drawings.AddChart("ParetoChart", OfficeOpenXml.Drawing.Chart.eChartType.ColumnClustered);
            chart.SetPosition(row + 2, 0, 0, 0);
            chart.SetSize(600, 400);
            chart.Title.Text = paretoChart.Title;
            
            var series = chart.Series.Add(worksheet.Cells[4, 2, row - 1, 2], worksheet.Cells[4, 1, row - 1, 1]);
            series.Header = "Frequency";

            return package.GetAsByteArray();
        });
    }
}
