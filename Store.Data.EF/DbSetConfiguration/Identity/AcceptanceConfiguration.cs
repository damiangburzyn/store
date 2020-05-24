using Store.Data.Extensions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class AcceptanceConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<Acceptance> entityBuilder)
        {
            entityBuilder.AddBaseEntityBuilder();


            entityBuilder.Property(x => x.Ip)
                .IsRequired()
                .HasMaxLength(15);
            

            entityBuilder.HasOne(p => p.ApplicationUser)
            .WithMany(b => b.Acceptances)
            .HasForeignKey(p => p.ApplicationUserId);

            entityBuilder.HasOne(p => p.AcceptanceFormula)
            .WithMany(b => b.Acceptances)
            .HasForeignKey(p => p.AcceptanceFormulaId);
        }
    }
}