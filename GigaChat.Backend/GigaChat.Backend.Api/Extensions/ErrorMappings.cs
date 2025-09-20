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
        "Conversation.UnauthorizedAction" => StatusCodes.Status403Forbidden,
        "Conversation.AccessDenied" => StatusCodes.Status403Forbidden,
        "Conversation.AlreadyExists" => StatusCodes.Status409Conflict,
        "Conversation.UserNotFound" => StatusCodes.Status404NotFound,
        "Conversation.NoParticipants" => StatusCodes.Status400BadRequest,
        "Conversation.SelfReference" => StatusCodes.Status400BadRequest,
        "Conversation.InvalidIdentifier" => StatusCodes.Status400BadRequest,
        "Conversation.BlockedByUser" => StatusCodes.Status403Forbidden,
        "Conversation.Spammer" => StatusCodes.Status403Forbidden,
        "Conversation.InviteRejected" => StatusCodes.Status400BadRequest,
        "Conversation.NotGroupAdmin" => StatusCodes.Status403Forbidden,
        "Conversation.InviteNotFound" => StatusCodes.Status404NotFound,
        "Conversation.CannotModifySelf" => StatusCodes.Status403Forbidden,
        "Conversation.TargetNotMember" => StatusCodes.Status400BadRequest,
        "Conversation.CannotRemoveFromPrivateChat" => StatusCodes.Status403Forbidden,
        "Block.SelfBlock" => StatusCodes.Status403Forbidden,
        "Conversation.BlockedUser" => StatusCodes.Status403Forbidden,
        _ => StatusCodes.Status400BadRequest // Default case
    };
}