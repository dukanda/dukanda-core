using DukandaCore.Application.Common.Constants;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Identity;

public record UpdateUserAvatarCommand : IRequest<Result<UserDto>>
{
    public Guid UserId { get; init; }
    public Stream ImageStream { get; init; } = null!;
    public string FileName { get; init; } = null!;
}

public class UpdateUserAvatarCommandHandler : IRequestHandler<UpdateUserAvatarCommand, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICloudinaryService _cloudinaryService;

    public UpdateUserAvatarCommandHandler(
        IApplicationDbContext context,
        ICloudinaryService cloudinaryService)
    {
        _context = context;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<Result<UserDto>> Handle(UpdateUserAvatarCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(request.UserId);

        if (user == null)
        {
            return Result.Failure<UserDto>(ErrorCodes.UserNotFound);
        }

        var avatarUrl = await _cloudinaryService.UploadFileAsync(request.ImageStream, request.FileName);
        
        user.AvatarUrl = avatarUrl;
        await _context.SaveChangesAsync(cancellationToken);

        return Result<UserDto>.Success(new UserDto(user));
    }
}
