namespace DukandaCore.Domain.Entities
{
    public class AttractionImage : BaseAuditableEntity
    {
        public Guid TouristAttractionId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public TouristAttraction TouristAttraction { get; set; } = null!;
    }
}