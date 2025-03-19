using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<PaymentMethod>
{
    public void Configure(EntityTypeBuilder<PaymentMethod> builder)
    {
        builder.HasData(
            new PaymentMethod 
            { 
                Id = 1, 
                Name = "Transferência Bancária", 
                Description = "Pagamento via transferência bancária",
                Color = "#4CAF50",  // Green
                Icon = "bank",
                DisplayOrder = 1
            },
            new PaymentMethod 
            { 
                Id = 2, 
                Name = "Visa", 
                Description = "Pagamento com cartão Visa",
                Color = "#2196F3",  // Blue
                Icon = "credit-card",
                DisplayOrder = 2
            },
            new PaymentMethod 
            { 
                Id = 3, 
                Name = "Multicaixa Express", 
                Description = "Pagamento via Multicaixa Express",
                Color = "#FF9800",  // Orange
                Icon = "mobile-alt",
                DisplayOrder = 3
            }
        );
    }
}