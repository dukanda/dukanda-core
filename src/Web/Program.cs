using DukandaCore.Infrastructure.Data;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.AddKeyVaultIfConfigured();
builder.AddApplicationServices();
builder.AddInfrastructureServices();
builder.AddWebServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
       
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var pendingMigrations = db.Database.GetPendingMigrations();
        if (pendingMigrations.Any())
        {
            Console.WriteLine("Migrating database ...");
            await db.Database.MigrateAsync();
        }
    }

    await app.InitialiseDatabaseAsync();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHealthChecks("/health");
app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Map("/", () => Results.Redirect("/swagger"));
app.UseSwagger();
app.UseSwaggerUI();

app.UseHangfireDashboard("/jobs", new DashboardOptions
{
    Authorization = new[] { new AllowAllConnectionsFilter() }
});

app.UseExceptionHandler(options => { });


app.Run();

public partial class Program
{
}