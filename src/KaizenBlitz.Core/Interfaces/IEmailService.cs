using KaizenBlitz.Core.Models;

namespace KaizenBlitz.Core.Interfaces;

/// <summary>
/// Service interface for email operations
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Send project report via email
    /// </summary>
    Task SendProjectReportAsync(string recipient, byte[] pdfAttachment, Project project);
}
