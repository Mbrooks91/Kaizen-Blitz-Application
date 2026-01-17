using Xunit;
using Microsoft.EntityFrameworkCore;
using KaizenBlitz.Data;
using KaizenBlitz.Data.Repositories;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;

namespace KaizenBlitz.Tests.UnitTests.Repositories;

public class ProjectRepositoryTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task CreateAsync_ValidProject_ReturnsCreatedProject()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        var project = new Project
        {
            Name = "Test Project",
            Description = "Test Description",
            TargetArea = "Manufacturing",
            StartDate = DateTime.Now,
            Status = ProjectStatus.InProgress
        };

        // Act
        var result = await repository.CreateAsync(project);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Project", result.Name);
        Assert.NotEqual(Guid.Empty, result.ProjectId);
    }

    [Fact]
    public async Task GetAllAsync_MultipleProjects_ReturnsAllProjects()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        
        await repository.CreateAsync(new Project { Name = "Project 1", StartDate = DateTime.Now });
        await repository.CreateAsync(new Project { Name = "Project 2", StartDate = DateTime.Now });
        await repository.CreateAsync(new Project { Name = "Project 3", StartDate = DateTime.Now });

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(3, result.Count());
    }

    [Fact]
    public async Task SearchAsync_MatchingTerm_ReturnsMatchingProjects()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        
        await repository.CreateAsync(new Project { Name = "Kaizen Project", StartDate = DateTime.Now });
        await repository.CreateAsync(new Project { Name = "Lean Project", StartDate = DateTime.Now });
        await repository.CreateAsync(new Project { Name = "Kaizen Improvement", StartDate = DateTime.Now });

        // Act
        var result = await repository.SearchAsync("Kaizen");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task DeleteAsync_ExistingProject_RemovesProject()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var repository = new ProjectRepository(context);
        var project = await repository.CreateAsync(new Project { Name = "To Delete", StartDate = DateTime.Now });

        // Act
        await repository.DeleteAsync(project.ProjectId);
        var allProjects = await repository.GetAllAsync();

        // Assert
        Assert.Empty(allProjects);
    }
}
