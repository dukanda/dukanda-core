using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
{
    public void Configure(EntityTypeBuilder<BookingStatus> builder)
    {
        builder.HasData(
            new BookingStatus 
            { 
                Id = 1, 
                Name = "Pendente", 
                Description = "Estado inicial quando a reserva é criada",
                Color = "#FFA500",  // Orange
                Icon = "clock",
                DisplayOrder = 1
            },
            new BookingStatus 
            { 
                Id = 2, 
                Name = "Reservado", 
                Description = "Vagas temporariamente reservadas",
                Color = "#4CAF50",  // Green
                Icon = "calendar-check",
                DisplayOrder = 2
            },
            new BookingStatus 
            { 
                Id = 3, 
                Name = "Pagamento Pendente", 
                Description = "Aguardando confirmação de pagamento",
                Color = "#FF9800",  // Deep Orange
                Icon = "credit-card",
                DisplayOrder = 3
            },
            new BookingStatus 
            { 
                Id = 4, 
                Name = "Confirmado", 
                Description = "Pagamento confirmado e reserva ativa",
                Color = "#2196F3",  // Blue
                Icon = "check-circle",
                DisplayOrder = 4
            },
            new BookingStatus 
            { 
                Id = 5, 
                Name = "Concluído", 
                Description = "Passeio foi completado",
                Color = "#9C27B0",  // Purple
                Icon = "check-double",
                DisplayOrder = 5
            },
            new BookingStatus 
            { 
                Id = 6, 
                Name = "Cancelado", 
                Description = "Reserva cancelada pelo usuário",
                Color = "#F44336",  // Red
                Icon = "times-circle",
                DisplayOrder = 6
            },
            new BookingStatus 
            { 
                Id = 7, 
                Name = "Expirado", 
                Description = "Reserva expirada (pagamento não recebido a tempo)",
                Color = "#795548",  // Brown
                Icon = "clock",
                DisplayOrder = 7
            },
            new BookingStatus 
            { 
                Id = 8, 
                Name = "Reembolsado", 
                Description = "Reserva foi reembolsada",
                Color = "#607D8B",  // Blue Grey
                Icon = "undo",
                DisplayOrder = 8
            },
            new BookingStatus 
            { 
                Id = 9, 
                Name = "Falha", 
                Description = "Falha no pagamento ou outro processo",
                Color = "#FF5722",  // Deep Orange
                Icon = "exclamation-triangle",
                DisplayOrder = 9
            }
        );
    }
}