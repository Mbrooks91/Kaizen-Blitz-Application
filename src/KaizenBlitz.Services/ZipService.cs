using KaizenBlitz.Core.Models;
using System.IO.Compression;

namespace KaizenBlitz.Services;

/// <summary>
/// Service for creating ZIP archives of project exports
/// </summary>
public class ZipService
{
    /// <summary>
    /// Create a ZIP archive containing all project exports
    /// </summary>
    public async Task<byte[]> CreateProjectArchiveAsync(Project project, Dictionary<string, byte[]> files)
    {
        return await Task.Run(() =>
        {
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var entry = archive.CreateEntry(file.Key);
                    using var entryStream = entry.Open();
                    entryStream.Write(file.Value, 0, file.Value.Length);
                }
            }

            return memoryStream.ToArray();
        });
    }
}
