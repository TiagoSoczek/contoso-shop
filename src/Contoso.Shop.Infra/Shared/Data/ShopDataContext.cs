using System;
using System.Data;
using System.Threading.Tasks;
using Contoso.Shop.Model.AccessControl;
using Contoso.Shop.Model.Catalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Contoso.Shop.Infra.Shared.Data
{
    public class ShopDataContext : DbContext
    {
        private IDbContextTransaction currentTransaction;

        public ShopDataContext(DbContextOptions options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<User> Users { get; set; }

        public void BeginTransaction()
        {
            if (currentTransaction != null)
            {
                return;
            }

            currentTransaction = Database.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                currentTransaction?.Commit();
            }
            catch (Exception)
            {
                RollbackTransaction();

                // TODO: Review this
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var modelMapping = new ModelMapping(modelBuilder);

            modelMapping.Map();
        }
    }
}