using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using Store.Data.EF.Extensions;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ProductDeliveryMethodConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<ProductDeliveryMethod> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Property(x => x.Price).HasPrecision(9, 2);
            entityBuilder.HasOne(a => a.Delivery)
                .WithMany(d => d.ProductDeliveryMethods).HasForeignKey(a => a.DeliveryId);

            entityBuilder
              .HasIndex(p => new { p.DeliveryId, p.ProductId }).IsUnique();
        }
    }
}
