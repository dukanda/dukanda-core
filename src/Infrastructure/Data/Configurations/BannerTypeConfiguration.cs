
using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class BannerTypeConfiguration : IEntityTypeConfiguration<BannerType>
{
    public void Configure(EntityTypeBuilder<BannerType> builder)
    {
        builder.HasData(
            new BannerType { Id = 1, Name = "Principal", Description = "Banner principal da página inicial", Icon = "star", DisplayOrder = 1 },
            new BannerType { Id = 2, Name = "Promoção", Description = "Banners promocionais", Icon = "percent", DisplayOrder = 2 },
            new BannerType { Id = 3, Name = "Destaque", Description = "Banners de destaque nas categorias", Icon = "spotlight", DisplayOrder = 3 }
        );
    }
}
