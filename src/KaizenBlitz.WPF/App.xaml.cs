using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
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
    private ServiceProvider? _serviceProvider;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            // Run database migrations
            using (var scope = _serviceProvider.CreateScope())
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
                    Console.WriteLine($"Error initializing database: {ex.Message}");
                }
            }

            // Show main window
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        }

        base.OnFrameworkInitializationCompleted();
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

    protected override void Dispose(bool disposing)
    {
        _serviceProvider?.Dispose();
        base.Dispose(disposing);
    }
}
