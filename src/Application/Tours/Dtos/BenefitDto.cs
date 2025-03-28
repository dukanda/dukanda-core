using DukandaCore.Domain.Entities;

public class BenefitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;

    public BenefitDto(Benefit benefit)
    {
        Id = benefit.Id;
        Name = benefit.Name;
        Description = benefit.Description;
    }
}