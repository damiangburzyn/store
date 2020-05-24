using System;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store.Data.EF.Entities;

namespace Store.Data.Extensions
{
    public static class EntityTypeBuilderExtension
    {
        public static EntityTypeBuilder<TEntity> AddBaseEntityBuilder<TEntity>(this EntityTypeBuilder<TEntity> entity)
            where TEntity : BaseEntity
        {
            entity.HasKey(t => t.Id);

            entity.Property(en => en.Id)
                .IsRequired();

            entity.Property(en => en.CreateDate)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            entity.Property(en => en.ModifyDate)
                .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                .IsRequired();

            return entity;
        }
    }
}