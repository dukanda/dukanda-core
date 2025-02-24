namespace DukandaCore.Domain.Identity;

public class Role: BaseEntity
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}