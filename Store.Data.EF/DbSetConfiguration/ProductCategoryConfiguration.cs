using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ProductsCategoriesConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<ProductCategory> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();


            entityBuilder.HasIndex(pc => new { pc.CategoryId, pc.ProductId });

            entityBuilder
                .HasOne(pc => pc.Category)
                .WithMany("ProductCategories")
                .HasForeignKey(cm => cm.CategoryId);

            entityBuilder
                .HasOne(pc => pc.Product)
                .WithMany("ProductCategories")
                .HasForeignKey(cm => cm.ProductId);
        }
    }
}
