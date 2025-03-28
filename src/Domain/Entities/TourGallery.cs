namespace DukandaCore.Domain.Entities
{
    public class TourGallery : BaseAuditableEntity
    {
        public Guid TourId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public Tour Tour { get; set; } = null!;
    }
} 