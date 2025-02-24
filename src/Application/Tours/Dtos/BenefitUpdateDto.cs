public class BenefitUpdateDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    public bool IsDeleted { get; init; }
}
