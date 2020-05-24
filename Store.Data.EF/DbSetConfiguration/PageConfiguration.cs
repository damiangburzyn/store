using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Store.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Data.EF.DbSetConfiguration
{
   
    internal static class PageConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<Page> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.HasIndex(b => b.PageName).IsUnique();
        }
    }
}
