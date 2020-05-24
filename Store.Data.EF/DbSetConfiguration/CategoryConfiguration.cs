using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class CategoryConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<Category> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Property(x => x.SortOrder)
               .IsRequired();

            entityBuilder.HasOne(c => c.ParentCategory).WithMany(c => c.SubCategories).HasForeignKey(c => c.ParentCategoryId);

            entityBuilder.HasMany(c => c.SubCategories).WithOne(c => c.ParentCategory);

            entityBuilder.Ignore(x => x.Products);

         
              
        }
    }
}
