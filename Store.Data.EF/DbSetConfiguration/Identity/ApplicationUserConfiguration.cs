using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.Entities.Identity;
using System;

namespace Store.Data.EF.DbSetConfiguration
{
    internal static class ApplicationUserConfiguration
    {
        internal static void GetConfiguration(this EntityTypeBuilder<ApplicationUser> entityBuilder)
        {
            entityBuilder.Property(x => x.NormalizedEmail)
                .HasMaxLength(256)
                .IsRequired();

            entityBuilder.Property(x => x.Email)
                .HasMaxLength(256)
                .IsRequired();

            entityBuilder.Property(x => x.NormalizedUserName)
                .HasMaxLength(24)
                .IsRequired();

            entityBuilder.Property(x => x.UserName)
                .HasMaxLength(24)
                .IsRequired();

            entityBuilder.Property(en => en.CreateDate)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            entityBuilder.Property(en => en.ModifyDate)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            entityBuilder.Property(en => en.RemovalDateUtc)
                .HasConversion(v => v, v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : (DateTime?)null);


            entityBuilder.HasIndex(p => new { p.NormalizedEmail })
                .IsUnique()
                .HasName("IX_AspNetUsers_NormalizedEmail");

            entityBuilder.HasIndex(p => new { p.Email })
                .IsUnique();

            entityBuilder.HasIndex(p => new { p.NormalizedUserName })
                .IsUnique()
                .HasName("IX_AspNetUsers_NormalizedUserName");

            entityBuilder.HasIndex(p => new { p.UserName })
                .IsUnique();

            entityBuilder.HasMany(p => p.Acceptances)
                .WithOne(a => a.ApplicationUser)
                .HasForeignKey(p => p.ApplicationUserId);
        }
    }
}