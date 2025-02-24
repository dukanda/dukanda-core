using System.Reflection;
using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Domain.Entities;
using DukandaCore.Domain.Identity;
using Microsoft.EntityFrameworkCore;

namespace DukandaCore.Infrastructure.Data;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<UserRole> UserRoles => Set<UserRole>();
    
    public DbSet<City> Cities => Set<City>();
    public DbSet<TouristAttraction> TouristAttractions => Set<TouristAttraction>();
    public DbSet<AttractionImage> AttractionImages => Set<AttractionImage>();
    public DbSet<TourAgency> TourAgencies => Set<TourAgency>();
    public DbSet<TourAgencyType> AgencyTypes => Set<TourAgencyType>();
    public DbSet<Tour> Tours => Set<Tour>();
    public DbSet<TourItinerary> TourItineraries => Set<TourItinerary>();
    public DbSet<TourType> TourTypes => Set<TourType>();
    public DbSet<Package> Packages => Set<Package>();
    public DbSet<Benefit> Benefits => Set<Benefit>();
    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<UserRole>()
            .HasKey(ur => new { ur.UserId, ur.RoleId });
            
        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UserId);
            
        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId);

        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
