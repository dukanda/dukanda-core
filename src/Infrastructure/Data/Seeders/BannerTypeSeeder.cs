using DukandaCore.Domain.Entities;

namespace DukandaCore.Infrastructure.Data.Seeders;

public class BannerTypeSeeder
{
    private readonly ApplicationDbContext _context;

    public BannerTypeSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedBannerTypesAsync()
    {
        if (_context.BannerTypes.Any()) return;

        var bannerTypes = new List<BannerType>
        {
            new()
            {
                Name = "Destaque Principal",
                Description = "Banners em destaque na página inicial",
                Icon = "fa-star",
                DisplayOrder = 1
            },
            new()
            {
                Name = "Promoções",
                Description = "Banners promocionais e ofertas especiais",
                Icon = "fa-tag",
                DisplayOrder = 2
            },
            new()
            {
                Name = "Eventos",
                Description = "Banners de eventos e festivais",
                Icon = "fa-calendar",
                DisplayOrder = 3
            },
            new()
            {
                Name = "Atrações",
                Description = "Banners de atrações turísticas",
                Icon = "fa-landmark",
                DisplayOrder = 4
            },
            new()
            {
                Name = "Informativo",
                Description = "Banners informativos e institucionais",
                Icon = "fa-info-circle",
                DisplayOrder = 5
            }
        };

        _context.BannerTypes.AddRange(bannerTypes);
        await _context.SaveChangesAsync();
    }
} 