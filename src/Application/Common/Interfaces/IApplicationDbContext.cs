using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Identity;

namespace DukandaCore.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; }
    DbSet<Role> Roles { get; }
    DbSet<UserRole> UserRoles { get; }

    DbSet<City> Cities { get; }
    DbSet<TouristAttraction> TouristAttractions { get; }
    DbSet<AttractionGallery> AttractionGalleries { get; }
    DbSet<TourAgency> TourAgencies { get; }
    DbSet<TourAgencyType> AgencyTypes { get; }
    DbSet<Tour> Tours { get; }
    DbSet<TourItinerary> TourItineraries { get; }
    DbSet<TourType> TourTypes { get; }
    DbSet<TourGallery> TourGalleries { get; }
    DbSet<Package> Packages { get; }
    DbSet<Benefit> Benefits { get; }
    DbSet<Booking> Bookings { get; }
    DbSet<Domain.Entities.News> News { get; }
    DbSet<NewsGallery> NewsGalleries { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}