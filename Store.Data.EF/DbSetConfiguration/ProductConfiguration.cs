using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ProductConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<Product> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Property(x => x.ConnectedProdIds)
                .HasConversion(v => string.Join(",", v),
                    v => Array.ConvertAll(v.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), long.Parse));

            entityBuilder.Ignore(x => x.Categories);
        }
    }
}
