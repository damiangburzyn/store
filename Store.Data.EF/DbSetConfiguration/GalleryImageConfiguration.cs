using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class GalleryImageConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<GalleryImage> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Property(x => x.Name)
                .IsRequired();
        }
    }
}
