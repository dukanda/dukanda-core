using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class Booking : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public Guid PackageId { get; set; }
        public int BookingStatusId { get; set; }
        public decimal Price { get; set; }
        public User User { get; set; } = null!;
        public Package Package { get; set; } = null!;
        public BookingStatus Status { get; set; } = null!;
    }
}