using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Microsoft.Extensions.Configuration;
using VehiclePartsApi.Domain.Interfaces;

namespace VehiclePartsApi.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    public EmailService(IConfiguration config) => _config = config;

    public async Task SendAsync(string toEmail, string subject, string htmlBody)
    {
        var host = _config["Smtp:Host"]!;
        var port = int.Parse(_config["Smtp:Port"]!);
        var user = _config["Smtp:User"]!;
        var pass = _config["Smtp:Pass"]!;
        var fromEmail = _config["Smtp:FromEmail"]!;
        var fromName = _config["Smtp:FromName"] ?? "Vehicle Parts Center";

        var msg = new MimeMessage();
        msg.From.Add(new MailboxAddress(fromName, fromEmail));
        msg.To.Add(new MailboxAddress("", toEmail));
        msg.Subject = subject;
        msg.Body = new BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();

        try 
        {
            using var client = new SmtpClient();
            client.Timeout = 10000; // 10 second timeout
            await client.ConnectAsync(host, port, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(user, pass);
            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"SMTP Error: {ex.Message}");
            throw;
        }
    }
}