# Building Kaizen Blitz Application on Windows

## Prerequisites

1. **Windows 10/11** - This is a WPF application and requires Windows
2. **.NET 6.0 SDK** - Download from: https://dotnet.microsoft.com/download/dotnet/6.0
3. **Visual Studio 2022** (recommended) or **VS Code** with C# Dev Kit

## Quick Build Instructions

### Option 1: Visual Studio 2022 (Recommended)

1. Clone or download this repository to your Windows machine
2. Open `src/KaizenBlitz.sln` in Visual Studio 2022
3. Wait for NuGet packages to restore automatically
4. Press `Ctrl+Shift+B` to build
5. Press `F5` to run the application

### Option 2: Command Line (PowerShell or CMD)

```powershell
# Navigate to the project root
cd path\to\Kaizen-Blitz-Application

# Restore NuGet packages
dotnet restore src\KaizenBlitz.sln

# Build the solution
dotnet build src\KaizenBlitz.sln --configuration Release

# Run the application
dotnet run --project src\KaizenBlitz.WPF
```

## Creating a Standalone .EXE

To create a self-contained executable that can run without .NET installed:

```powershell
dotnet publish src\KaizenBlitz.WPF `
  --configuration Release `
  --runtime win-x64 `
  --self-contained true `
  --output publish\win-x64
```

The executable will be at: `publish\win-x64\KaizenBlitz.WPF.exe`

For a framework-dependent version (requires .NET 6.0 installed):

```powershell
dotnet publish src\KaizenBlitz.WPF `
  --configuration Release `
  --runtime win-x64 `
  --self-contained false `
  --output publish\win-x64-framework
```

## First Run

On first run, the application will:
1. Create a SQLite database file: `kaizenblitz.db`
2. Run all migrations automatically
3. Seed initial data if configured

## Project Structure

```
src/
├── KaizenBlitz.Core/        # Domain models and interfaces
├── KaizenBlitz.Data/        # Entity Framework and repositories
├── KaizenBlitz.Services/    # Business logic and export services
├── KaizenBlitz.WPF/         # WPF user interface (Windows-only)
└── KaizenBlitz.Tests/       # Unit tests
```

## Features

- **Project Management**: Create and manage Kaizen blitz events
- **Quality Tools**:
  - Five Whys analysis
  - Ishikawa (Fishbone) diagrams
  - Action planning with task tracking
  - Pareto charts
- **Export Capabilities**:
  - PDF reports (QuestPDF)
  - Word documents
  - Excel spreadsheets
- **Data Persistence**: SQLite database with EF Core

## Troubleshooting

### Build Errors

If you see errors about missing packages:
```powershell
dotnet restore src\KaizenBlitz.sln --force
```

### Runtime Errors

If the application doesn't start:
1. Ensure .NET 6.0 Runtime is installed
2. Check if `appsettings.json` exists in the output directory
3. Look for errors in the console output

### Database Issues

To reset the database, simply delete `kaizenblitz.db` and restart the application.

## Support

For issues or questions, refer to:
- [README.md](README.md) - Main documentation
- [ARCHITECTURE.md](docs/ARCHITECTURE.md) - Technical architecture
- [USER_GUIDE.md](docs/USER_GUIDE.md) - User guide
- [CONTRIBUTING.md](CONTRIBUTING.md) - Contribution guidelines
