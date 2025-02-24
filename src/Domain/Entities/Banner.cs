namespace DukandaCore.Domain.Entities
{
    public class Banner : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsFeatured { get; set; }
        public string LinkUrl { get; set; } = null!;
        public int BannerTypeId { get; set; }
        public int DisplayOrder { get; set; }

        public BannerType BannerType { get; set; } = null!;
    }
}
