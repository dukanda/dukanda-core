using DukandaCore.Application.Common.Interfaces;
using DukandaCore.Domain.Constants;
using DukandaCore.Domain.Identity;
using DukandaCore.Infrastructure.Data;
using DukandaCore.Infrastructure.Data.Interceptors;
using DukandaCore.Infrastructure.Email;
using DukandaCore.Infrastructure.FileStorage;
using DukandaCore.Infrastructure.Identity;
using DukandaCore.Infrastructure.QrCode;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DukandaCoreDb");
        Guard.Against.Null(connectionString, message: "Connection string 'DukandaCoreDb' not found.");

        builder.Services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        builder.Services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            options.UseNpgsql(connectionString);
        });


        builder.Services.AddScoped<IApplicationDbContext>(provider =>
            provider.GetRequiredService<ApplicationDbContext>());
        builder.Services.AddScoped<ApplicationDbContextInitialiser>();

        builder.Services.AddHangfire(config => config
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseSQLiteStorage("hangfire.db"));

        builder.Services.AddHangfireServer();

        builder.Services.AddSingleton(TimeProvider.System);
        builder.Services.AddTransient<IAuthService, AuthService>();
        builder.Services.AddTransient<ITokenService, TokenService>();
        builder.Services.AddTransient<IPasswordHasher, PasswordHasher>();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<ICloudinaryService, CloudinaryService>();
        builder.Services.AddTransient<IQrCodeService, QrCodeService>();

        builder.Services.AddAuthorization(options =>
            options.AddPolicy(Policies.CanPurge, policy => policy.RequireRole(Roles.Administrator)));
    }
}