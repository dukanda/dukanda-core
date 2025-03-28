using DukandaCore.Domain.Enums;
using DukandaCore.Domain.Identity;

namespace DukandaCore.Domain.Entities
{
    public class Booking : BaseAuditableEntity
    {
        public Guid UserId { get; set; }
        public string BookingNumber { get; set; } = null!;
        public int BookingStatusId { get; set; }
        public decimal TotalPrice { get; set; }
        public string PaymentIntentId { get; set; } = string.Empty;
        public DateTimeOffset? PaymentDate { get; set; }
        public string? QrCodeUrl { get; set; }
        public string? CancellationReason { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }

        public User User { get; set; } = null!;
        
        public PaymentIntent PaymentIntent { get; set; } = null!;
        public BookingStatus BookingStatus { get; set; } = null!;
        public ICollection<BookingItem> Items { get; set; } = new List<BookingItem>();
    }
}