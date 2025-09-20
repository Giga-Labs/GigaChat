using System.ComponentModel.DataAnnotations;

namespace GigaChat.Backend.Domain.Enums.Identity;

public enum OtpPurpose
{
    [Display(Name = "Password Reset")]
    PasswordReset,

    [Display(Name = "Email Verification")]
    EmailVerification,

    [Display(Name = "Two-Factor Authentication")]
    TwoFactorAuth
}