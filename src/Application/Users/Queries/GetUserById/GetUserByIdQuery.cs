using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using MediatR;

namespace DukandaCore.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid UserId) : IRequest<Result<UserDto>>;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserDto>>
{
    private readonly IAuthService _authService;

    public GetUserByIdQueryHandler(IAuthService authService)
    {
        _authService = authService;
    }

    public async Task<Result<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _authService.GetUserByIdAsync(request.UserId);
            return Result<UserDto>.Success(user);
        }
        catch (InvalidOperationException ex)
        {
            return Result.Failure<UserDto>(ex.Message);
        }
    }
} 