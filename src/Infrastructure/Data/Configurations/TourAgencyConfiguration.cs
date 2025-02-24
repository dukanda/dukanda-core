using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TourAgencyConfiguration : IEntityTypeConfiguration<TourAgency>
{
    public void Configure(EntityTypeBuilder<TourAgency> builder)
    {
        builder.HasKey(t => t.UserId);

        builder.HasOne(t => t.User)
            .WithOne(u => u.TourAgency)
            .HasForeignKey<TourAgency>(t => t.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
