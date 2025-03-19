namespace DukandaCore.Domain.Entities;

public class PaymentIntent : BaseEntity
{
    public Guid BookingId { get; set; }
    public Booking Booking { get; set; } = null!;
    
    public int PaymentMethodId { get; set; }
    
    public decimal Amount { get; set; }
    
    public string? TransactionReference { get; set; }
    public string MetaData { get; set; } = null!;
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public DateTime? CompletedAt { get; set; }
    
    public PaymentMethod PaymentMethod { get; set; } = null!;
}
