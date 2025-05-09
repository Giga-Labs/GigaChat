using GigaChat.Backend.Domain.Abstractions;

namespace GigaChat.Backend.Application.Errors;

public static class ConversationErrors
{
    public static readonly Error AccessDenied =
        new("Conversation.AccessDenied", "You can’t access this conversation.");

    public static readonly Error AlreadyExists =
        new("Conversation.AlreadyExists", "A conversation with the same participants already exists.");

    public static readonly Error UserNotFound =
        new("Conversation.UserNotFound", "One or more specified users were not found.");

    public static readonly Error EmptyParticipantList =
        new("Conversation.NoParticipants", "You must include at least one other user.");

    public static readonly Error SelfNotAllowed =
        new("Conversation.SelfReference", "You cannot create a conversation with yourself.");

    public static readonly Error InvalidIdentifier =
        new("Conversation.InvalidIdentifier", "Each user identifier must be a valid email or username.");

    public static readonly Error InviteeBlockedYou =
        new("Conversation.BlockedByUser", "You cannot start a conversation because the user has blocked you.");

    public static readonly Error SpamThresholdReached =
        new("Conversation.Spammer", "You’ve been reported too many times and can’t create new conversations.");

    public static readonly Error InviteRejected =
        new("Conversation.InviteRejected", "This invite has already been rejected and cannot be re-sent.");

    public static readonly Error NotGroupAdmin =
        new("Conversation.NotGroupAdmin", "Only group admins can perform this action.");
    
    public static readonly Error InviteNotFound =
        new("Conversation.InviteNotFound", "No invitation found for this user in the specified conversation.");
    
    public static readonly Error NotFound =
        new("Conversation.NotFound", "No conversation found with this specified id.");
}