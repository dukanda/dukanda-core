using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using MediatR;

namespace DukandaCore.Application.Users.Queries.GetCurrentUser;

public record GetCurrentUserQuery : IRequest<Result<UserDto>>;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, Result<UserDto>>
{
    private readonly IUser _currentUser;
    private readonly IAuthService _authService;

    public GetCurrentUserQueryHandler(IUser currentUser, IAuthService authService)
    {
        _currentUser = currentUser;
        _authService = authService;
    }

    public async Task<Result<UserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (_currentUser.Id == null)
        {
            return Result.Failure<UserDto>("User not authenticated");
        }

        try
        {
            var user = await _authService.GetUserByIdAsync(_currentUser.Id.Value);
            return Result<UserDto>.Success(user);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure<UserDto>(ex.Message);
        }
    }
} 