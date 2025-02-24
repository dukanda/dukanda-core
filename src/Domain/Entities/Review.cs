using DukandaCore.Domain.Common;
using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class Review : BaseAuditableEntity
    {
        public string Comment { get; set; } = null!;
        public int Rating { get; set; }
        public Guid UserId { get; set; }
        public Guid? TourId { get; set; }
        public Guid? TouristAttractionId { get; set; }

        public User User { get; set; } = null!;
        public Tour? Tour { get; set; }
        public TouristAttraction? TouristAttraction { get; set; }
    }
}
