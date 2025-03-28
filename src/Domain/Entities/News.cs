using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class News : BaseAuditableEntity
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string CoverImageUrl { get; set; } = null!;
        public bool IsFeatured { get; set; }
        public DateTimeOffset? PublishedAt { get; set; }
        public Guid? PublishedById { get; set; }
        public int ViewCount { get; set; }
        public string? Tags { get; set; }
        
        public User? PublishedBy { get; set; }
        public ICollection<NewsGallery> Gallery { get; set; } = new List<NewsGallery>();
    }
} 