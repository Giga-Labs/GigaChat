using GigaChat.Backend.Domain.Entities.Core;

namespace GigaChat.Backend.Application.Repositories.Core;

public interface IReportedInviteRepository
{
    Task AddAsync(ReportedInvite report, CancellationToken cancellationToken = default);
    Task<int> CountReportsAgainstUserAsync(string inviterId, CancellationToken cancellationToken = default);
    Task<bool> HasInviteBeenReportedAsync(string inviterId, string inviteeId, CancellationToken cancellationToken = default);
}

