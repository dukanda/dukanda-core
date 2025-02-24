using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Domain.Identity;
using DukandaCore.Infrastructure.Data.Seeders;
using DukandaCore.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DukandaCore.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;

    public ApplicationDbContextInitialiser(ApplicationDbContext context,
        IPasswordHasher passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task InitialiseAsync()
    {
        var tourSeeder = new TourSeeder(_context);
        await tourSeeder.SeedToursAsync();
        await _context.Database.MigrateAsync();
        await SeedDefaultUserAsync();
    }

    private async Task SeedDefaultUserAsync()
    {
        if (!_context.Users.Any())
        {
            var defaultUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Sys",
                LastName = "Administrator",
                UserName = "admin@localhost",
                Email = "admin@localhost",
                PhoneNumber = "0123456789",
                AvatarUrl = "",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                PasswordHash = _passwordHasher.HashPassword("admin123"),
            };

            _context.Users.Add(defaultUser);
            await _context.SaveChangesAsync();
        }
    }
}