using GigaChat.Backend.Domain.Abstractions;

namespace GigaChat.Backend.Application.Errors;

public static class UserErrors
{
    public static readonly Error InvalidCredentials = new("User.InvalidCredentials", "Invalid credentials.");
    public static readonly Error UserNotFound = new("User.NotFound", "User not found.");
    public static readonly Error DuplicateEmail = new("User.DuplicateEmail", "A user with this email already exists.");
    public static readonly Error DuplicateUserName = new("User.DuplicateUserName", "A user with this username already exists.");
    public static readonly Error RegistrationFailed = new("User.RegistrationFailed", "Registration failed.");
    public static readonly Error EmailNotConfirmed = new("User.EmailNotConfirmed", "Email is not confirmed.");
    public static readonly Error InvalidEmailConfirmationToken = new("User.InvalidEmailConfirmationToken", "Invalid email confirmation token."); // the token as a whole though like the Jwt
    public static readonly Error EmailAlreadyConfirmed = new("User.EmailAlreadyConfirmed", "The email is already confirmed.");
    public static readonly Error EmailConfirmationFailed = new("User.EmailConfirmationFailed", "Email confirmation failed.");
    public static readonly Error ResetPasswordFailed = new("User.ResetPasswordFailed", "Reset password failed.");
    public static readonly Error InvalidPasswordResetOtp = new("User.InvalidPasswordResetOtp", "Invalid or expired OTP.");
    public static readonly Error InvalidPasswordResetToken = new("User.InvalidPasswordResetToken", "Invalid or expired password reset token.");
    public static readonly Error PasswordChangeFailed = new("User.PasswordChangeFailed", "Failed to change the user's password. Please try again.");
    
}