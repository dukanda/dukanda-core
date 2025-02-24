using DukandaCore.Domain.Identity;

public record UserDto
{
    public Guid Id { get; init; }
    public string Email { get; init; } = null!;
    public string? AvatarUrl { get; init; }
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;
    public string PhoneNumber { get; init; } = null!;
    public DateTimeOffset Created { get; init; }

    public UserDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
        FirstName = user.FirstName;
        LastName = user.LastName;
        PhoneNumber = user.PhoneNumber;
        Created = user.Created;
        AvatarUrl = user.AvatarUrl;
    }
}
