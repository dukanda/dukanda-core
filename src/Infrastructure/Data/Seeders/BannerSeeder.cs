using DukandaCore.Domain.Entities;

namespace DukandaCore.Infrastructure.Data.Seeders;

public class BannerSeeder
{
    private readonly ApplicationDbContext _context;

    public BannerSeeder(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task SeedBannersAsync()
    {
        if (_context.Banners.Any()) return;

        var now = DateTime.UtcNow;
        var banners = new List<Banner>
        {
            new()
            {
                Title = "Descubra Angola",
                Description = "Explore as maravilhas naturais e culturais de Angola",
                ImageUrl = "https://images.unsplash.com/photo-1516026672322-bc52d61a55d5",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = true,
                LinkUrl = "/destinations",
                BannerTypeId =1,
                DisplayOrder = 1
            },
            new()
            {
                Title = "Praias Paradisíacas",
                Description = "Conheça as melhores praias de Luanda",
                ImageUrl = "https://images.unsplash.com/photo-1507525428034-b723cf961d3e",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = true,
                LinkUrl = "/tours/list",
                BannerTypeId = 2,
                DisplayOrder = 2
            },
            new()
            {
                Title = "Roteiros Culturais",
                Description = "Mergulhe na rica cultura angolana",
                ImageUrl = "https://images.unsplash.com/photo-1533669955142-6a73332af4db",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 3,
                DisplayOrder = 3
            },
            new()
            {
                Title = "Safaris e Aventuras",
                Description = "Aventure-se pelos parques nacionais de Angola",
                ImageUrl = "https://images.unsplash.com/photo-1516426122078-c23e76319801",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 1,
                DisplayOrder = 4
            },
            new()
            {
                Title = "Gastronomia Local",
                Description = "Saboreie os melhores pratos típicos angolanos",
                ImageUrl = "https://images.unsplash.com/photo-1504674900247-0877df9cc836",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 2,
                DisplayOrder = 5
            },
            new()
            {
                Title = "Festivais Tradicionais",
                Description = "Participe das festas e celebrações angolanas",
                ImageUrl = "https://images.unsplash.com/photo-1533294455009-a77b7557d2d1",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 3,
                DisplayOrder = 6
            },
            new()
            {
                Title = "Artesanato Local",
                Description = "Descubra o artesanato tradicional de Angola",
                ImageUrl = "https://images.unsplash.com/photo-1490535004195-099bc723fa1f",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 1,
                DisplayOrder = 7
            },
            new()
            {
                Title = "Ecoturismo",
                Description = "Explore a natureza preservada de Angola",
                ImageUrl = "https://images.unsplash.com/photo-1441974231531-c6227db76b6e",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 2,
                DisplayOrder = 8
            },
            new()
            {
                Title = "Roteiros Históricos",
                Description = "Conheça a história fascinante de Angola",
                ImageUrl = "https://images.unsplash.com/photo-1461988320302-91bde64fc8e4",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 3,
                DisplayOrder = 9
            },
            new()
            {
                Title = "Experiências Únicas",
                Description = "Viva momentos inesquecíveis em Angola",
                ImageUrl = "https://images.unsplash.com/photo-1469474968028-56623f02e42e",
                StartDate = now,
                EndDate = now.AddMonths(3),
                IsActive = true,
                IsFeatured = false,
                LinkUrl = "/tours/list",
                BannerTypeId = 1,
                DisplayOrder = 10
            }
        };

        _context.Banners.AddRange(banners);
        await _context.SaveChangesAsync();
    }
} 