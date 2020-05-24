using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class AcceptanceTypeConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<AcceptanceType> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.HasKey(x => x.Id);

            entityBuilder.Property(x => x.Id)
              .ValueGeneratedNever();

            entityBuilder.Property(x => x.Name)
                .IsRequired();
            
        }
    }
}