using KaizenBlitz.Core.Interfaces;
using KaizenBlitz.Core.Models;
using System.Diagnostics;

namespace KaizenBlitz.Services;

/// <summary>
/// Service for sending emails
/// </summary>
public class EmailService : IEmailService
{
    public async Task SendProjectReportAsync(string recipient, byte[] pdfAttachment, Project project)
    {
        await Task.Run(() =>
        {
            try
            {
                // Save PDF to temp file
                var tempPath = Path.Combine(Path.GetTempPath(), $"KaizenBlitz_{project.Name}_{DateTime.Now:yyyyMMdd}.pdf");
                File.WriteAllBytes(tempPath, pdfAttachment);

                // Create mailto link
                var subject = Uri.EscapeDataString($"Kaizen Blitz Project Report: {project.Name}");
                var body = Uri.EscapeDataString($"Please find attached the Kaizen Blitz project report for '{project.Name}'.\n\nGenerated on: {DateTime.Now:yyyy-MM-dd HH:mm}");
                
                var mailtoUrl = $"mailto:{recipient}?subject={subject}&body={body}";
                
                // Open default email client
                var psi = new ProcessStartInfo
                {
                    FileName = mailtoUrl,
                    UseShellExecute = true
                };
                Process.Start(psi);

                // Note: The attachment cannot be automatically added via mailto protocol
                // The user will need to manually attach the file from the temp location
                // Display a message to the user with the file path
                Console.WriteLine($"PDF saved to: {tempPath}");
                Console.WriteLine("Please manually attach this file to your email.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error opening email client: {ex.Message}");
                throw;
            }
        });
    }
}
