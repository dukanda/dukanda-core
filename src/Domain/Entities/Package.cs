using DukandaCore.Domain.Common;

namespace DukandaCore.Domain.Entities
{
    public class Package : BaseAuditableEntity
    {
        public Guid TourId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int MaxParticipants { get; set; }
        public int MaxSlots { get; set; }
        public int AvailableSlots { get; set; }
        public bool IsFullyBooked => AvailableSlots <= 0;

        public Tour Tour { get; set; } = null!;
        public List<Benefit> Benefits { get; set; } = new();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
