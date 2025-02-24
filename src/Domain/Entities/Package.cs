using DukandaCore.Domain.Common;

namespace DukandaCore.Domain.Entities
{
    public class Package : BaseAuditableEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }
        public Guid TourId { get; set; }

        public Tour Tour { get; set; } = null!;
        public List<Benefit> Benefits { get; set; } = new();
        public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
