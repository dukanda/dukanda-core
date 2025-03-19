namespace DukandaCore.Domain.Entities
{
    public class NewsGallery : BaseAuditableEntity
    {
        public Guid NewsId { get; set; }
        public string ImageUrl { get; set; } = null!;
        public string Caption { get; set; } = null!;
        public int DisplayOrder { get; set; }

        public News News { get; set; } = null!;
    }
} 