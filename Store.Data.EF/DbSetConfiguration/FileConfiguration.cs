using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;
using Microsoft.EntityFrameworkCore;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class FileConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<File> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();

            entityBuilder.Ignore(f => f.SortOrder);
        }
    }
}
