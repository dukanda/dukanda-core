using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.HasData(
            new City { Id = 1, Name = "Luanda", Description = "Capital de Angola", ImageUrl = "luanda.jpg" },
            new City { Id = 2, Name = "Benguela", Description = "Província costeira conhecida por suas praias", ImageUrl = "benguela.jpg" },
            new City { Id = 3, Name = "Huíla", Description = "Província do planalto central", ImageUrl = "huila.jpg" },
            new City { Id = 4, Name = "Cabinda", Description = "Província do norte de Angola", ImageUrl = "cabinda.jpg" },
            new City { Id = 5, Name = "Huambo", Description = "Antiga Nova Lisboa", ImageUrl = "huambo.jpg" },
            new City { Id = 6, Name = "Malanje", Description = "Terra das Quedas de Kalandula", ImageUrl = "malanje.jpg" },
            new City { Id = 7, Name = "Namibe", Description = "Província do deserto do Namibe", ImageUrl = "namibe.jpg" },
            new City { Id = 8, Name = "Zaire", Description = "Província histórica do norte", ImageUrl = "zaire.jpg" },
            new City { Id = 9, Name = "Uíge", Description = "Terra do café", ImageUrl = "uige.jpg" },
            new City { Id = 10, Name = "Cunene", Description = "Província do sul de Angola", ImageUrl = "cunene.jpg" },
            new City { Id = 11, Name = "Bié", Description = "Província do planalto central", ImageUrl = "bie.jpg" },
            new City { Id = 12, Name = "Cuando Cubango", Description = "A maior província de Angola", ImageUrl = "cuando-cubango.jpg" },
            new City { Id = 13, Name = "Cuanza Norte", Description = "Província histórica", ImageUrl = "cuanza-norte.jpg" },
            new City { Id = 14, Name = "Cuanza Sul", Description = "Terra do café robusta", ImageUrl = "cuanza-sul.jpg" },
            new City { Id = 15, Name = "Lunda Norte", Description = "Província dos diamantes", ImageUrl = "lunda-norte.jpg" },
            new City { Id = 16, Name = "Lunda Sul", Description = "Província mineira", ImageUrl = "lunda-sul.jpg" },
            new City { Id = 17, Name = "Moxico", Description = "A segunda maior província", ImageUrl = "moxico.jpg" },
            new City { Id = 18, Name = "Bengo", Description = "Província próxima à capital", ImageUrl = "bengo.jpg" }
        );
    }
}
