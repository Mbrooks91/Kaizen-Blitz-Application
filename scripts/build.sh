#!/bin/bash
# Build and Publish Script for Kaizen Blitz Application

set -e

# Default values
CONFIGURATION="Release"
RUNTIME="win-x64"
SELF_CONTAINED="true"

# Parse command line arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --configuration)
            CONFIGURATION="$2"
            shift 2
            ;;
        --runtime)
            RUNTIME="$2"
            shift 2
            ;;
        --self-contained)
            SELF_CONTAINED="$2"
            shift 2
            ;;
        *)
            echo "Unknown option: $1"
            exit 1
            ;;
    esac
done

echo "================================================"
echo "Kaizen Blitz Application Build Script"
echo "================================================"
echo ""

# Get script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" &> /dev/null && pwd )"
ROOT_PATH="$(dirname "$SCRIPT_DIR")"
SRC_PATH="$ROOT_PATH/src"
SOLUTION_FILE="$SRC_PATH/KaizenBlitz.sln"
PUBLISH_PATH="$ROOT_PATH/publish"
WPF_PROJECT="$SRC_PATH/KaizenBlitz.WPF/KaizenBlitz.WPF.csproj"

echo "Configuration: $CONFIGURATION"
echo "Runtime: $RUNTIME"
echo "Self-Contained: $SELF_CONTAINED"
echo ""

# Clean previous builds
echo "Cleaning previous builds..."
if [ -d "$PUBLISH_PATH" ]; then
    rm -rf "$PUBLISH_PATH"
fi

# Restore dependencies
echo "Restoring NuGet packages..."
dotnet restore "$SOLUTION_FILE"

# Build solution
echo "Building solution..."
dotnet build "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-restore

# Run tests
echo "Running tests..."
dotnet test "$SOLUTION_FILE" --configuration "$CONFIGURATION" --no-build --verbosity normal

# Publish application
echo "Publishing application..."
dotnet publish "$WPF_PROJECT" \
    --configuration "$CONFIGURATION" \
    --runtime "$RUNTIME" \
    --self-contained "$SELF_CONTAINED" \
    --output "$PUBLISH_PATH" \
    /p:PublishSingleFile=false \
    /p:IncludeNativeLibrariesForSelfExtract=true

echo ""
echo "================================================"
echo "Build completed successfully!"
echo "================================================"
echo ""
echo "Published files location:"
echo "$PUBLISH_PATH"
echo ""
echo "To run the application on Windows:"
echo "cd $PUBLISH_PATH"
echo "./KaizenBlitz.WPF.exe"
echo ""
