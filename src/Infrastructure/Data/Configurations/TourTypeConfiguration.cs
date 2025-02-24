using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MyNamespace
{
    
}

public class TourTypeConfiguration : IEntityTypeConfiguration<TourType>
{
    public void Configure(EntityTypeBuilder<TourType> builder)
    {
        builder.HasData(
            new TourType { Id = 1, Name = "Aventura", Description = "Tours de aventura e esportes radicais", Icon = "mountain", DisplayOrder = 1 },
            new TourType { Id = 2, Name = "Cultural", Description = "Tours culturais e históricos", Icon = "museum", DisplayOrder = 2 },
            new TourType { Id = 3, Name = "Gastronômico", Description = "Experiências gastronômicas locais", Icon = "utensils", DisplayOrder = 3 },
            new TourType { Id = 4, Name = "Natureza", Description = "Tours em parques e reservas naturais", Icon = "tree", DisplayOrder = 4 },
            new TourType { Id = 5, Name = "Praia", Description = "Tours em praias e atividades marítimas", Icon = "umbrella-beach", DisplayOrder = 5 }
        );
    }
}
