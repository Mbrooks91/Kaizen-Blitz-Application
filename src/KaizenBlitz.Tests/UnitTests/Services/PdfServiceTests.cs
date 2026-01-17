using Xunit;
using Moq;
using KaizenBlitz.Services;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Tests.UnitTests.Services;

public class PdfServiceTests
{
    private readonly PdfService _pdfService;

    public PdfServiceTests()
    {
        _pdfService = new PdfService();
    }

    [Fact]
    public async Task GenerateProjectSummaryAsync_ValidProject_ReturnsPdfBytes()
    {
        // Arrange
        var project = new Project
        {
            ProjectId = Guid.NewGuid(),
            Name = "Test Project",
            Description = "Test Description",
            TargetArea = "Manufacturing",
            StartDate = DateTime.Now,
            Status = ProjectStatus.InProgress,
            ProgressPercentage = 50,
            TeamMembers = "[\"John Doe\", \"Jane Smith\"]",
            CurrentPhase = KaizenPhase.Analysis
        };

        // Act
        var result = await _pdfService.GenerateProjectSummaryAsync(project);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
        Assert.True(result[0] == 0x25); // PDF file signature starts with %PDF
    }

    [Fact]
    public async Task GenerateFiveWhysPdfAsync_ValidFiveWhys_ReturnsPdfBytes()
    {
        // Arrange
        var fiveWhys = new FiveWhys
        {
            FiveWhysId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            ProblemStatement = "Production line stopped",
            Why1 = "Machine overheated",
            Why2 = "Cooling system failed",
            Why3 = "Filter was clogged",
            Why4 = "Maintenance schedule not followed",
            Why5 = "No tracking system for maintenance",
            RootCause = "Lack of preventive maintenance system",
            IsCompleted = true
        };

        // Act
        var result = await _pdfService.GenerateFiveWhysPdfAsync(fiveWhys);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }

    [Fact]
    public async Task GenerateActionPlanPdfAsync_ValidActionPlan_ReturnsPdfBytes()
    {
        // Arrange
        var actionPlan = new ActionPlan
        {
            ActionPlanId = Guid.NewGuid(),
            ProjectId = Guid.NewGuid(),
            IsCompleted = false,
            Tasks = new List<ActionPlanTask>
            {
                new ActionPlanTask
                {
                    TaskDescription = "Implement new process",
                    ResponsiblePerson = "John Doe",
                    Deadline = DateTime.Now.AddDays(30),
                    Status = TaskStatus.InProgress,
                    Priority = "High"
                }
            }
        };

        // Act
        var result = await _pdfService.GenerateActionPlanPdfAsync(actionPlan);

        // Assert
        Assert.NotNull(result);
        Assert.True(result.Length > 0);
    }
}
