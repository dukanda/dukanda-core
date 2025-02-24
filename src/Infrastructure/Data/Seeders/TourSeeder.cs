using Bogus;
using Bogus.Extensions;
using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Infrastructure.Data.Seeders
{
    public class TourSeeder
    {
        private readonly DbContext _context;
        private readonly Random _random;

        public TourSeeder(DbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedToursAsync(int numberOfTours = 1000)
        {
            // Delete existing tours and related entities
            await DeleteExistingToursAndDependencies();

            // Ensure we have prerequisite data
            var cities = await _context.Set<City>().ToListAsync();
            var agencies = await _context.Set<TourAgency>().ToListAsync();
            var tourTypes = await _context.Set<TourType>().ToListAsync();
            var attractions = await _context.Set<TouristAttraction>().ToListAsync();
            var users = await _context.Set<User>().ToListAsync();

            if (!cities.Any() || !agencies.Any() || !tourTypes.Any() || !users.Any())
            {
                throw new InvalidOperationException("Prerequisite data must be seeded first.");
            }

            var tourFaker = new Faker<Tour>()
                .RuleFor(t => t.Id, f => Guid.NewGuid())
                .RuleFor(t => t.Title, f => f.Lorem.Sentence(3, 2))
                .RuleFor(t => t.Description, f => f.Lorem.Paragraph())
                .RuleFor(t => t.CoverImageUrl, f => f.Image.PicsumUrl())
                .RuleFor(t => t.BasePrice, f => f.Random.Decimal(100, 5000))
                .RuleFor(t => t.StartDate, f => f.Date.Future().ToUniversalTime())
                .RuleFor(t => t.EndDate, (f, t) => t.StartDate.AddDays(f.Random.Int(3, 14)))
                .RuleFor(t => t.AgencyId, f => agencies[f.Random.Int(0, agencies.Count - 1)].UserId)
                .RuleFor(t => t.CityId, f => cities[f.Random.Int(0, cities.Count - 1)].Id)
                .RuleFor(t => t.IsFeatured, f => f.Random.Bool(0.2f))
                .RuleFor(t => t.TourTypes, f =>
                {
                    var selectedTypesCount = f.Random.Int(1, Math.Min(3, tourTypes.Count));
                    return f.Random.ListItems(tourTypes, selectedTypesCount).ToList();
                })
                .RuleFor(t => t.Attractions, f =>
                {
                    var selectedAttractionsCount = f.Random.Int(1, Math.Min(5, attractions.Count));
                    return f.Random.ListItems(attractions, selectedAttractionsCount).ToList();
                })
                // Randomly publish some tours
                .RuleFor(t => t.PublishedAt, f =>
                    f.Random.Bool(0.6f) ? f.Date.Past().ToUniversalTime() : (DateTimeOffset?)null)
                .RuleFor(t => t.PublishedById, (f, t) =>
                    t.PublishedAt.HasValue
                        ? users[f.Random.Int(0, users.Count - 1)].Id
                        : (Guid?)null);

            var tours = tourFaker.Generate(numberOfTours);

            // Add Packages and Itineraries for each Tour
            foreach (var tour in tours)
            {
                tour.Itineraries = GenerateItinerariesForTour(tour);
                tour.Packages = GeneratePackagesForTour(tour);
            }

            _context.Set<Tour>().AddRange(tours);
            await _context.SaveChangesAsync();
        }

        private async Task DeleteExistingToursAndDependencies()
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Benefits\" CASCADE");
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Packages\" CASCADE");
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"TourItineraries\" CASCADE");
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Tours\" CASCADE");
        }

        private List<TourItinerary> GenerateItinerariesForTour(Tour tour)
        {
            var itineraryFaker = new Faker<TourItinerary>()
                .RuleFor(i => i.Id, f => Guid.NewGuid())
                .RuleFor(i => i.TourId, tour.Id)
                .RuleFor(i => i.Date, (f, i) =>
                    tour.StartDate.AddDays(f.Random.Int(0, (tour.EndDate - tour.StartDate).Days)).ToUniversalTime())
                .RuleFor(i => i.Title, f => f.Lorem.Sentence(2, 1))
                .RuleFor(i => i.Description, f => f.Lorem.Paragraph())
                .RuleFor(i => i.DisplayOrder, (f, i) => f.Random.Int(1, 10));

            return itineraryFaker.Generate(_random.Next(3, 8));
        }


        private List<Package> GeneratePackagesForTour(Tour tour)
        {
            var packageFaker = new Faker<Package>()
                .RuleFor(p => p.Id, f => Guid.NewGuid())
                .RuleFor(p => p.TourId, tour.Id)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Price, f => f.Random.Decimal2(5000, 100000))
                .RuleFor(p => p.Benefits, f =>
                {
                    var benefitFaker = new Faker<Benefit>()
                        .RuleFor(b => b.Id, f => Guid.NewGuid())
                        .RuleFor(b => b.Name, f => f.Commerce.ProductAdjective())
                        .RuleFor(b => b.Description, f => f.Lorem.Sentence());

                    return benefitFaker.Generate(_random.Next(1, 5));
                });

            return packageFaker.Generate(_random.Next(1, 4));
        }
    }
}
