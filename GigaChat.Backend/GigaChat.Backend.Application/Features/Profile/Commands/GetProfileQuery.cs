using GigaChat.Backend.Application.Features.Profile.Contracts;
using GigaChat.Backend.Domain.Abstractions;
using MediatR;

namespace GigaChat.Backend.Application.Features.Profile.Commands;

public record GetProfileQuery(string RequesterId) : IRequest<Result<UserProfileResponse>>;