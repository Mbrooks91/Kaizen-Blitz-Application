# Kaizen-Blitz-Application
Kaizen Blitz Application for Windows

## Quick Start

### Building the Application

You can build using the provided bash script:

```bash
# Make script executable (first time only)
chmod +x scripts/build.sh

# Run the build script
./scripts/build.sh
```

Or build manually:

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

Using the build script:
```bash
./scripts/build.sh --configuration Release --runtime win-x64 --self-contained true
```

Or manually:
```bash
dotnet publish src/KaizenBlitz.WPF -c Release -r win-x64 --self-contained
```

See the full [README documentation](README.md) for complete details.
