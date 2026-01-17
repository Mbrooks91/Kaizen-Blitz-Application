using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using KaizenBlitz.Core.Models;
using KaizenBlitz.Core.Models.Enums;
using System.Text.Json;

namespace KaizenBlitz.Data;

/// <summary>
/// Database context for Kaizen Blitz application
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    // DbSets
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<FiveWhys> FiveWhys => Set<FiveWhys>();
    public DbSet<IshikawaDiagram> IshikawaDiagrams => Set<IshikawaDiagram>();
    public DbSet<IshikawaCategory> IshikawaCategories => Set<IshikawaCategory>();
    public DbSet<ActionPlan> ActionPlans => Set<ActionPlan>();
    public DbSet<ActionPlanTask> ActionPlanTasks => Set<ActionPlanTask>();
    public DbSet<ParetoChart> ParetoCharts => Set<ParetoChart>();
    public DbSet<ParetoItem> ParetoItems => Set<ParetoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Project
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
            entity.Property(e => e.TeamMembers).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<string>(v, (JsonSerializerOptions?)null) ?? "[]"
            );

            // One-to-one relationships
            entity.HasOne(e => e.FiveWhys)
                .WithOne(e => e.Project)
                .HasForeignKey<FiveWhys>(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.IshikawaDiagram)
                .WithOne(e => e.Project)
                .HasForeignKey<IshikawaDiagram>(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ActionPlan)
                .WithOne(e => e.Project)
                .HasForeignKey<ActionPlan>(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.ParetoChart)
                .WithOne(e => e.Project)
                .HasForeignKey<ParetoChart>(e => e.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure FiveWhys
        modelBuilder.Entity<FiveWhys>(entity =>
        {
            entity.HasKey(e => e.FiveWhysId);
            entity.Property(e => e.ProblemStatement).IsRequired().HasMaxLength(1000);
            entity.Property(e => e.AdditionalWhys).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<string>(v, (JsonSerializerOptions?)null) ?? "[]"
            );
        });

        // Configure IshikawaDiagram
        modelBuilder.Entity<IshikawaDiagram>(entity =>
        {
            entity.HasKey(e => e.IshikawaId);
            entity.Property(e => e.ProblemStatement).IsRequired().HasMaxLength(1000);

            entity.HasMany(e => e.Categories)
                .WithOne(e => e.IshikawaDiagram)
                .HasForeignKey(e => e.IshikawaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure IshikawaCategory
        modelBuilder.Entity<IshikawaCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Causes).HasConversion(
                v => JsonSerializer.Serialize(v, (JsonSerializerOptions?)null),
                v => JsonSerializer.Deserialize<string>(v, (JsonSerializerOptions?)null) ?? "[]"
            );
        });

        // Configure ActionPlan
        modelBuilder.Entity<ActionPlan>(entity =>
        {
            entity.HasKey(e => e.ActionPlanId);

            entity.HasMany(e => e.Tasks)
                .WithOne(e => e.ActionPlan)
                .HasForeignKey(e => e.ActionPlanId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ActionPlanTask
        modelBuilder.Entity<ActionPlanTask>(entity =>
        {
            entity.HasKey(e => e.TaskId);
            entity.Property(e => e.TaskDescription).IsRequired().HasMaxLength(500);
        });

        // Configure ParetoChart
        modelBuilder.Entity<ParetoChart>(entity =>
        {
            entity.HasKey(e => e.ParetoChartId);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            entity.HasMany(e => e.Items)
                .WithOne(e => e.ParetoChart)
                .HasForeignKey(e => e.ParetoChartId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        // Configure ParetoItem
        modelBuilder.Entity<ParetoItem>(entity =>
        {
            entity.HasKey(e => e.ParetoItemId);
            entity.Property(e => e.Category).IsRequired().HasMaxLength(200);
        });
    }

    /// <summary>
    /// Seed initial data
    /// </summary>
    public void SeedData()
    {
        if (!Projects.Any())
        {
            var sampleProject = new Project
            {
                ProjectId = Guid.NewGuid(),
                Name = "Sample Kaizen Project",
                Description = "This is a sample project to demonstrate the Kaizen Blitz application features.",
                TargetArea = "Manufacturing Floor",
                StartDate = DateTime.Now.AddDays(-7),
                ExpectedCompletionDate = DateTime.Now.AddDays(14),
                Status = ProjectStatus.InProgress,
                ProgressPercentage = 30,
                TeamMembers = "[\"John Doe\", \"Jane Smith\", \"Bob Johnson\"]",
                CurrentPhase = KaizenPhase.Analysis,
                CreatedDate = DateTime.Now.AddDays(-7),
                ModifiedDate = DateTime.Now
            };

            Projects.Add(sampleProject);
            SaveChanges();
        }
    }
}
