using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class GalleryConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<Gallery> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Property(x => x.Name)
                .IsRequired();

            entityBuilder.HasMany(g => g.Images).WithOne().OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Cascade);
        }
    }
}
