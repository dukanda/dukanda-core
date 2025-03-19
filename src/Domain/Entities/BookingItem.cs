namespace DukandaCore.Domain.Entities
{
    public class BookingItem : BaseAuditableEntity
    {
        public Guid BookingId { get; set; }
        public Guid PackageId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }

        public Booking Booking { get; set; } = null!;
        public Package Package { get; set; } = null!;
    }
} 