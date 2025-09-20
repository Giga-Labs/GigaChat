using System.Net.Mail;
using GigaChat.Backend.Application.Services.Email;
using GigaChat.Backend.Infrastructure.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace GigaChat.Backend.Infrastructure.Services.Email;

public class EmailService(IOptions<MailSettings> mailSettings,IOptions<AppSettings> appSettings, ILogger<EmailService> logger) : IEmailService
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(mailSettings.Value.SenderName, mailSettings.Value.SenderEmail));
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            message.Body = new TextPart("html")
            {
                Text = htmlMessage
            };

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(mailSettings.Value.SmtpServer, mailSettings.Value.Port,
                SecureSocketOptions.StartTls);
            await smtpClient.AuthenticateAsync(mailSettings.Value.UserName, mailSettings.Value.Password);
            await smtpClient.SendAsync(message);
            await smtpClient.DisconnectAsync(true);
            
            logger.LogInformation("Sending an email to {email}", email);
        
            logger.LogInformation("📩 Email sent successfully to {email}", email);
        }
        catch (Exception e)
        {
            logger.LogError("❌ Failed to send email: {message}", e.Message);
        }
    }
    
    public async Task SendConfirmationEmail(string email, string token)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation", new Dictionary<string, string>
        {
            {"{{link}}", $"{appSettings.Value.BaseUrl}/emailConfirmation?token={token}"}
        });

        await SendEmailAsync(email, "GigaChat: Email Confirmation", emailBody);
    }
    

    public async Task SendPasswordResetEmailAsync(string email, string otp)
    {
        var emailBody = EmailBodyBuilder.GenerateEmailBody("PasswordReset", new Dictionary<string, string>
        {
            {"{{otp_code}}", otp},
        });

        await SendEmailAsync(email, $"{otp} - GigaChat: Reset Password", emailBody);
    }
    
}