namespace GigaChat.Backend.Application.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
    Task SendConfirmationEmail(string email, string token);
    Task SendPasswordResetEmailAsync(string email, string otp);
}