using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ProductFileConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<ProductFile> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.HasIndex(cm => new { cm.ProductId, cm.FileId });

            entityBuilder
                .HasOne(cm => cm.Product)
                .WithMany("ProductFiles")
                .HasForeignKey(cm => cm.ProductId);

            entityBuilder
                .HasOne(cm => cm.File)
                .WithMany("ProductFiles")
                .HasForeignKey(cm => cm.FileId);

            entityBuilder.Property(pf => pf.SortOrder).IsRequired();
        }
    }
}
