using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Application.Common.Models;
using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Events;
using DukandaCore.Domain.Identity;

public record RegisterUserCommand : IRequest<Result<UserDto>>
{
    public string Email { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<UserDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IEmailService _emailService;
    private readonly IPasswordHasher _passwordHasher;

    public RegisterUserCommandHandler(IApplicationDbContext context, IEmailService emailService, 
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _emailService = emailService;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<UserDto>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.Email,
            FirstName = request.Name,
            LastName = "",
            PhoneNumber = request.PhoneNumber,
            PasswordHash = _passwordHasher.HashPassword(request.Password),
            AvatarUrl = "",
            SecurityStamp = Guid.NewGuid().ToString(),
            IsActive = true
        };

        user.AddDomainEvent(new UserCreatedEvent(user));
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken);

        return Result<UserDto>.Success(new UserDto(user));
    }
}