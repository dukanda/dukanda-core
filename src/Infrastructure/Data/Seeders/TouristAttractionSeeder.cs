using Bogus;
using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Infrastructure.Data.Seeders
{
    public class TouristAttractionSeeder
    {
        private readonly DbContext _context;
        private readonly Random _random;

        public TouristAttractionSeeder(DbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedTouristAttractionsAsync(int numberOfAttractions = 100)
        {
            // Ensure cities exist
            var cities = await _context.Set<City>().ToListAsync();
            if (!cities.Any())
            {
                throw new InvalidOperationException("Cities must be seeded first.");
            }

            var attractionFaker = new Faker<TouristAttraction>()
                .RuleFor(a => a.Id, f => Guid.NewGuid())
                .RuleFor(a => a.Name, f => f.Address.City())
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
                .RuleFor(a => a.ImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(a => a.IsFeatured, f => f.Random.Bool(0.2f))
                .RuleFor(a => a.CityId, f => cities[f.Random.Int(0, cities.Count - 1)].Id)
                .RuleFor(a => a.Latitude, f => f.Address.Latitude())
                .RuleFor(a => a.Longitude, f => f.Address.Longitude());

            var attractions = attractionFaker.Generate(numberOfAttractions);

            _context.Set<TouristAttraction>().AddRange(attractions);
            await _context.SaveChangesAsync();
        }
    }
}
