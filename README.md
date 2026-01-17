# Kaizen-Blitz-Application
Kaizen Blitz Application for Windows (**WPF - Windows Only**)

## 📥 Download

**[Download the latest Windows executable here](../../releases/latest)**

Two versions available:
- **Standalone** (~100MB) - Recommended. Runs without installing .NET
- **Framework** (~10MB) - Requires [.NET 6.0 Runtime](https://dotnet.microsoft.com/download/dotnet/6.0)

Just download, extract, and run `KaizenBlitz.WPF.exe`!

## ⚠️ Platform Requirements

**This application requires Windows to build and run.** It uses WPF (Windows Presentation Foundation), which is a Windows-only UI framework.

- **Supported**: Windows 10/11
- **Not Supported**: Linux, macOS (WPF is Windows-exclusive)

## Quick Start

### For End Users

**Just want to use the application?**

1. Go to [Releases](../../releases/latest)
2. Download `KaizenBlitz-win-x64-standalone.zip`
3. Extract and run `KaizenBlitz.WPF.exe`

No installation or setup required!

### For Developers

**Want to build from source?**

Prerequisites:
- Windows 10 or later
- .NET 6.0 SDK or later ([Download here](https://dotnet.microsoft.com/download/dotnet/6.0))
- Visual Studio 2022 (recommended) or VS Code with C# Dev Kit

### Building the Application

**On Windows**, you can build using Visual Studio or the command line:

#### Using Visual Studio 2022:
1. Open `src/KaizenBlitz.sln`
2. Press `Ctrl+Shift+B` to build
3. Press `F5` to run

#### Using Command Line:

```bash
# Navigate to source directory
cd src

# Restore packages
dotnet restore

# Build solution
dotnet build KaizenBlitz.sln

# Run application
dotnet run --project KaizenBlitz.WPF
```

### Building for Release

Build a release executable:
```bash
dotnet publish src/KaizenBlitz.WPF -c Release -r win-x64 --self-contained
```

The executable will be at:
```
src/KaizenBlitz.WPF/bin/Release/net6.0-windows/win-x64/publish/KaizenBlitz.WPF.exe
```
