using GigaChat.Backend.Application.Repositories.Core;
using GigaChat.Backend.Domain.Entities.Core;
using GigaChat.Backend.Infrastructure.Persistence.Core;
using Microsoft.EntityFrameworkCore;

namespace GigaChat.Backend.Infrastructure.Repositories.Core;

public class ReportedInviteRepository(CoreDbContext coreDbContext) : IReportedInviteRepository
{
    public async Task AddAsync(ReportedInvite report, CancellationToken cancellationToken = default)
    {
        await coreDbContext.ReportedInvites.AddAsync(report, cancellationToken);
        await coreDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> CountReportsAgainstUserAsync(string inviterId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ReportedInvites
            .CountAsync(r => r.InviterId == inviterId, cancellationToken);
    }

    public async Task<bool> HasInviteBeenReportedAsync(string inviterId, string inviteeId, CancellationToken cancellationToken = default)
    {
        return await coreDbContext.ReportedInvites
            .AnyAsync(r => r.InviterId == inviterId && r.InviteeId == inviteeId, cancellationToken);
    }
}