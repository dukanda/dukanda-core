namespace DukandaCore.Domain.Entities{
public class TourType : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public int DisplayOrder { get; set; }
    
    public ICollection<Tour> Tours { get; set; } = new List<Tour>();
}
}