using EDA.Services.Order.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;
using EDA.Services.Order.Entities.Base;

namespace EDA.Services.Order
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Entities.Order>? Orders { get; set; }
        public DbSet<CartItem>? CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetRelationsAndIndexes(modelBuilder);
            ApplyGlobalIsDeletedFilter(modelBuilder);
        }


        private static void SetRelationsAndIndexes(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.Order>()
                .HasMany(o => o.Cart)
                .WithOne(ci => ci.Order)
                .HasForeignKey(ci => ci.OrderId);
        }

        private static void ApplyGlobalIsDeletedFilter(ModelBuilder modelBuilder)
        {
            Expression<Func<AuditableEntity, bool>> expression = entity => !entity.IsDeleted;
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.BaseType != typeof(AuditableEntity)) continue;

                var newParam = Expression.Parameter(entityType.ClrType);
                var newBody =
                    ReplacingExpressionVisitor.Replace(expression.Parameters.Single(), newParam, expression.Body);
                modelBuilder.Entity(entityType.ClrType).HasQueryFilter(Expression.Lambda(newBody, newParam));
            }
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditProperties();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default)
        {
            UpdateAuditProperties();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public override int SaveChanges()
        {
            UpdateAuditProperties();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditProperties();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void UpdateAuditProperties()
        {
            var now = DateTime.UtcNow;

            ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added)
                .Select(e => e.Entity as AuditableEntity).Where(e => e != null)
                .ToList()
                .ForEach(e => e.CreatedDateUtc = now);

            ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified)
                .Select(e => e.Entity as AuditableEntity).Where(e => e != null)
                .ToList()
                .ForEach(e => e.ModifiedDateUtc = now);
        }
    }
}
