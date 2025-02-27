using Bogus;
using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Infrastructure.Data.Seeders
{
    public class TourAgencySeeder
    {
        private readonly DbContext _context;
        private readonly Random _random;

        public TourAgencySeeder(DbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedTourAgenciesAsync(int numberOfAgencies = 50)
        {
            // Ensure tour agency types exist
            var agencyTypes = await _context.Set<TourAgencyType>().ToListAsync();
            if (!agencyTypes.Any())
            {
                throw new InvalidOperationException("Tour Agency Types must be seeded first.");
            }

            var userFaker = new Faker<User>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.AvatarUrl, f => f.Image.PicsumUrl())
                .RuleFor(u => u.EmailConfirmed, true)
                .RuleFor(u => u.IsActive, true);

            var agencyFaker = new Faker<TourAgency>()
                .RuleFor(a => a.UserId, f => Guid.NewGuid())
                .RuleFor(a => a.Name, f => f.Company.CompanyName())
                .RuleFor(a => a.Description, f => f.Lorem.Paragraph())
                .RuleFor(a => a.ContactEmail, f => f.Internet.Email())
                .RuleFor(a => a.ContactPhone, f => f.Phone.PhoneNumber())
                .RuleFor(a => a.Address, f => f.Address.StreetAddress())
                .RuleFor(a => a.LogoUrl, f => f.Image.PicsumUrl())
                .RuleFor(a => a.IsVerified, f => f.Random.Bool(0.6f))
                .RuleFor(a => a.TourAgencyTypeId, f => agencyTypes[f.Random.Int(0, agencyTypes.Count - 1)].Id);

            var agencies = agencyFaker.Generate(numberOfAgencies);
            var users = agencies.Select(a => new User
            {
                Id = a.UserId,
                FirstName = agencyFaker.Generate().Name.Split(' ')[0],
                LastName = agencyFaker.Generate().Name.Split(' ')[1],
                UserName = agencyFaker.Generate().Name.Replace(" ", "").ToLower(),
                Email = agencyFaker.Generate().ContactEmail,
                PhoneNumber = agencyFaker.Generate().ContactPhone,
                AvatarUrl = agencyFaker.Generate().LogoUrl,
                EmailConfirmed = true,
                IsActive = true
            }).ToList();

            _context.Set<User>().AddRange(users);
            _context.Set<TourAgency>().AddRange(agencies);
            await _context.SaveChangesAsync();
        }
    }
}
