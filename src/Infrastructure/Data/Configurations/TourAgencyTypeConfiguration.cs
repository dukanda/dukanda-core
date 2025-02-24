using DukandaCore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DukandaCore.Infrastructure.Persistence.Configurations
{
    public class TourAgencyTypeConfiguration : IEntityTypeConfiguration<TourAgencyType>
    {
        public void Configure(EntityTypeBuilder<TourAgencyType> builder)
        {
            builder.HasData(
                new TourAgencyType { Id = 1, Name = "Individual", Description = "Guias turísticos autônomos e independentes", Icon = "user", DisplayOrder = 1 },
                new TourAgencyType { Id = 2, Name = "Empresa", Description = "Agências e operadoras de turismo", Icon = "building", DisplayOrder = 2 }
            );
        }
    }
}