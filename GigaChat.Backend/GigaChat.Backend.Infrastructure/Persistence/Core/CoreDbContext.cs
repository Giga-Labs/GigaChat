using GigaChat.Backend.Domain.Entities.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Persistence.Core;

public class CoreDbContext(DbContextOptions<CoreDbContext> options) : DbContext(options)
{
    public DbSet<BlockedUser> BlockedUsers { get; set; }
    public DbSet<ClearedConversation> ClearedConversations { get; set; }
    public DbSet<Conversation> Conversations { get; set; }
    public DbSet<ConversationInviteLog> ConversationInviteLogs { get; set; }
    public DbSet<ConversationMember> ConversationMembers { get; set; }
    public DbSet<DeletedMessage> DeletedMessages { get; set; }
    public DbSet<FileUploadMetadata> FileUploadMetadatas { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<MessageEditHistory> MessageEditHistories { get; set; }
    public DbSet<MessageReaction> MessageReactions { get; set; }
    public DbSet<MessageReceipt> MessageReceipts { get; set; }
    public DbSet<PinnedMessage> PinnedMessages { get; set; }
    public DbSet<ReportedInvite> ReportedInvites { get; set; }
    public DbSet<UserSettings> UserSettings { get; set; }
    public DbSet<UserSpamScore> UserSpamScores { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(InfrastructureAssemblyMarker).Assembly,
            x => x.Namespace != null && x.Namespace.Contains("Core"));
        
        base.OnModelCreating(modelBuilder);
    }
}