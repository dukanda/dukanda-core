
using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BookingStatusConfiguration : IEntityTypeConfiguration<BookingStatus>
{
    public void Configure(EntityTypeBuilder<BookingStatus> builder)
    {
        builder.HasData(
            new BookingStatus { Id = 1, Name = "Pendente", Description = "Reserva aguardando confirmação", Icon = "clock", Color = "#FFA500", DisplayOrder = 1 },
            new BookingStatus { Id = 2, Name = "Confirmado", Description = "Reserva confirmada", Icon = "check", Color = "#008000", DisplayOrder = 2 },
            new BookingStatus { Id = 3, Name = "Cancelado", Description = "Reserva cancelada", Icon = "x", Color = "#FF0000", DisplayOrder = 3 },
            new BookingStatus { Id = 4, Name = "Concluído", Description = "Tour realizado", Icon = "flag", Color = "#0000FF", DisplayOrder = 4 }
        );
    }
}
