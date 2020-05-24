using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class AcceptanceFormulaConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<AcceptanceFormula> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();


            entityBuilder.Property(x => x.JsonText)
                .IsRequired();
            

            entityBuilder.HasOne(p => p.AcceptanceType)
                .WithMany(b => b.AcceptanceFormulas)
                .HasForeignKey(p => p.AcceptanceTypeId);
        }
    }
}