using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KaizenBlitz.Data;
using KaizenBlitz.Data.Repositories;
using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Services;
using KaizenBlitz.WPF.Views;
using KaizenBlitz.WPF.ViewModels;
using KaizenBlitz.WPF.Helpers;

namespace KaizenBlitz.WPF;

public partial class App : Application
{
    public static ServiceProvider? ServiceProvider { get; private set; }

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();
        ConfigureServices(services);
        ServiceProvider = services.BuildServiceProvider();

        // Run database migrations
        using (var scope = ServiceProvider.CreateScope())
        {
            try
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
                
                // Optionally seed data
                db.SeedData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing database: {ex.Message}", "Database Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

    private void ConfigureServices(IServiceCollection services)
    {
        // Configuration
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        services.AddSingleton<IConfiguration>(configuration);

        // Database
        var connectionString = configuration.GetConnectionString("DefaultConnection") ?? "Data Source=kaizenblitz.db";
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));

        // Repositories
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IToolRepository, ToolRepository>();

        // Services
        services.AddScoped<IPdfService, PdfService>();
        services.AddScoped<IExportService, WordExportService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<ExcelExportService>();
        services.AddSingleton<ZipService>();

        // Helpers
        services.AddSingleton<NavigationService>();

        // ViewModels
        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<ProjectDashboardViewModel>();
        services.AddTransient<ProjectWizardViewModel>();
        services.AddTransient<FiveWhysViewModel>();
        services.AddTransient<IshikawaViewModel>();
        services.AddTransient<ActionPlanViewModel>();

        // Views
        services.AddTransient<MainWindow>();
        services.AddTransient<Dashboard.ProjectDashboardView>();
        services.AddTransient<Wizard.ProjectWizardView>();
        services.AddTransient<Tools.FiveWhysView>();
        services.AddTransient<Tools.IshikawaView>();
        services.AddTransient<Tools.ActionPlanView>();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        (ServiceProvider as IDisposable)?.Dispose();
        base.OnExit(e);
    }
}

