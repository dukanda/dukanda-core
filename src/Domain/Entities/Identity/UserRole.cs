namespace DukandaCore.Domain.Identity;

public class UserRole
{
    public Guid UserId { get; set; } = default!;
    public int RoleId { get; set; } = default!;
    public User User { get; set; } = default!;
    public Role Role { get; set; } = default!;
    public DateTime Created { get; set; }
}
