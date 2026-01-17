# How to Generate Your Windows .EXE Download

Your repository is now set up to automatically build Windows executables using GitHub Actions. Here's how to create a downloadable release:

## Method 1: Create a Release with a Tag (Recommended)

This will automatically build and publish a release with download links.

### Steps:

1. **Go to your GitHub repository**: https://github.com/Mbrooks91/Kaizen-Blitz-Application

2. **Click on "Releases"** in the right sidebar (or go to https://github.com/Mbrooks91/Kaizen-Blitz-Application/releases)

3. **Click "Create a new release"**

4. **Click "Choose a tag"** and type a version number like `v1.0.0` (must start with 'v')

5. **Set the release title** (e.g., "Kaizen Blitz v1.0.0")

6. **Click "Publish release"**

7. **Wait 5-10 minutes** - GitHub Actions will automatically:
   - Build the Windows .exe files
   - Create ZIP files
   - Attach them to your release

8. **Your users can now download** from: https://github.com/Mbrooks91/Kaizen-Blitz-Application/releases/latest

## Method 2: Manual Build (For Testing)

To build without creating a public release:

1. **Go to Actions tab**: https://github.com/Mbrooks91/Kaizen-Blitz-Application/actions

2. **Click "Manual Build"** in the left sidebar

3. **Click "Run workflow"** button (top right)

4. **Enter a version number** (e.g., 1.0.0)

5. **Click "Run workflow"**

6. **Wait for completion** (5-10 minutes)

7. **Download the artifacts**:
   - Click on the completed workflow run
   - Scroll to "Artifacts" section at the bottom
   - Download the ZIP files

## What Gets Built?

Two versions are created:

### 1. Standalone Version (~100MB)
- **File**: `KaizenBlitz-win-x64-standalone.zip`
- **Includes**: Everything needed to run (no .NET installation required)
- **Best for**: End users who want to just download and run

### 2. Framework Version (~10MB)
- **File**: `KaizenBlitz-win-x64-framework.zip`
- **Requires**: .NET 6.0 Runtime installed on user's computer
- **Best for**: Users who already have .NET installed

## Updating Your README Download Link

The README already has a download link that points to:
```
https://github.com/Mbrooks91/Kaizen-Blitz-Application/releases/latest
```

Once you create your first release (v1.0.0), this link will automatically work!

## Next Release

When you want to release an update:

1. Make your code changes
2. Commit and push to main
3. Create a new release with a new tag (e.g., `v1.1.0`)
4. GitHub Actions automatically builds the new version
5. Users can download the latest version

## Troubleshooting

**If the build fails:**
1. Go to Actions tab
2. Click on the failed workflow
3. Check the error messages in the logs
4. Most common issues:
   - Build errors in the code
   - Missing dependencies
   - Test failures

**If you don't see the artifacts:**
- Wait a few more minutes (builds take 5-10 minutes)
- Check the Actions tab to see if the workflow is still running
- Look for a green checkmark when complete
