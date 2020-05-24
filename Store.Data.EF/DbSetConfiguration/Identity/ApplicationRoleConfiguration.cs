using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ApplicationRoleConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<ApplicationRole> entityBuilder)
        {
            entityBuilder.Property(x => x.NormalizedName)
                .IsRequired();

            entityBuilder.Property(x => x.Name)
                .IsRequired();


            entityBuilder.HasIndex(p => new { p.NormalizedName })
                .IsUnique()
                .HasName("IX_AspNetRoles_NormalizedEmail");

            entityBuilder.HasIndex(p => new { p.Name })
                .IsUnique();
        }
    }
}
