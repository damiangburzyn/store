using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Store.Data.EF.DbSetConfiguration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Store.Data.EF.Entities;
using Store.Data.Entities.Identity;

namespace Store.Data.Database
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, long>
    {
        /// <summary>
        ///     Application database context
        ///     Use 'add-migration {migration-name} -Context ApplicationDbContext' in package manager console for adding new
        ///     migrations
        /// </summary>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
        {
        }



        public DbSet<Acceptance> Acceptances { get; set; }
        public DbSet<AcceptanceFormula> AcceptanceFormulas { get; set; }
        public DbSet<AcceptanceType> AcceptanceTypes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Gallery> Galleries { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart>Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<DeliveryMethod> DeliveryMethods { get; set; }

        public DbSet<Page> Pages { get; set; }

      


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>().GetConfiguration();
            builder.Entity<ApplicationRole>().GetConfiguration();

            builder.Entity<Page>().GetConfiguration();

            builder.Entity<Acceptance>().GetConfiguration();
            builder.Entity<AcceptanceFormula>().GetConfiguration();
            builder.Entity<AcceptanceType>().GetConfiguration();

            builder.Entity<ProductCategory>().GetConfiguration();
            builder.Entity<Category>().GetConfiguration();
          
            builder.Entity<Gallery>().GetConfiguration();
            builder.Entity<GalleryImage>().GetConfiguration();
            builder.Entity<Product>().GetConfiguration();
            
            builder.Entity<ProductFile>().GetConfiguration();
            builder.Entity<File>().GetConfiguration();
            //builder.Entity<DeliveryMethod>().GetConfiguration();

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            SetDateProperties(ChangeTracker);
            RunOperateAndValidate(ChangeTracker);

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            SetDateProperties(ChangeTracker);
            RunOperateAndValidate(ChangeTracker);

            return await base.SaveChangesAsync(cancellationToken);
        }

        private static void SetDateProperties(ChangeTracker changeTracker)
        {
            var changeSet = changeTracker.Entries<IBaseEntity>();

            if (changeSet == null)
            {
                return;
            }

            foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreateDate = DateTime.UtcNow;
                }
                else
                {
                    entry.Property(x => x.CreateDate).IsModified = false;
                }

                entry.Entity.ModifyDate = DateTime.UtcNow;
            }
        }

        private static void RunOperateAndValidate(ChangeTracker changeTracker)
        {
            var changeSet = changeTracker.Entries<IBaseEntity>();

            if (changeSet == null)
            {
                return;
            }

            foreach (var entry in changeSet.Where(c => c.State != EntityState.Unchanged))
            {
                //entry.Entity.Operate();
                //entry.Entity.Validate();
            }
        }
    }
}