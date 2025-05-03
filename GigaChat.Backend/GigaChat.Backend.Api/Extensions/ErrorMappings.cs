using System.Runtime.InteropServices.JavaScript;
using GigaChat.Backend.Domain.Abstractions;

namespace GigaChat.Backend.Api.Extensions;

public static class ErrorMappings
{
    public static int ToStatusCode(this Error error) => error.Code switch
    {
        "User.InvalidCredentials" => StatusCodes.Status401Unauthorized,
        "User.NotFound" => StatusCodes.Status404NotFound,
        "User.DuplicateEmail" => StatusCodes.Status409Conflict,
        "User.DuplicateUserName" => StatusCodes.Status409Conflict,
        "User.EmailNotConfirmed" => StatusCodes.Status403Forbidden,
        "User.InvalidEmailConfirmationCode" => StatusCodes.Status403Forbidden,
        "User.EmailAlreadyConfirmed" => StatusCodes.Status400BadRequest,
        "Token.Invalid" => StatusCodes.Status401Unauthorized,
        "User.ResetPasswordFailed" => StatusCodes.Status400BadRequest,
        "User.InvalidPasswordResetOtp" => StatusCodes.Status401Unauthorized,
        "User.InvalidPasswordResetToken" => StatusCodes.Status401Unauthorized,
        _ => StatusCodes.Status400BadRequest // Default case
    };
}