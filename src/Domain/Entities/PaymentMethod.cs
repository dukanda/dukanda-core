namespace DukandaCore.Domain.Entities;

public class PaymentMethod : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Color { get; set; } = null!;
    public string Icon { get; set; } = null!;
    public int DisplayOrder { get; set; }

    public ICollection<PaymentIntent> PaymentIntents { get; set; } = new List<PaymentIntent>();
}
