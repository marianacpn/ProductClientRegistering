using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using Persistence.EFCore.ClientFeature.Configuration;
using Persistence.EFCore.ProductFeature.Configuration;
using Shared.Config;
using System;
using System.Linq;

namespace Persistence.EFCore
{
    public class ProductClientContext : DbContext
    {
        private IDbContextTransaction transaction;
        private bool _disposed;
        private readonly DbConnectionConfig dbConnection;
        public ProductClientContext(IOptionsSnapshot<DbConnectionConfig> config)
        {
            dbConnection = config.Value;

            try
            {
                if (Database.GetPendingMigrations().Count() > 0)
                    Database.Migrate();
            }
            catch (Exception)
            {

            }
            ChangeTracker.LazyLoadingEnabled = false;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(dbConnection.GetConnectionString());

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        public void Commit()
        {
            transaction?.Commit();
            transaction = null;
        }

        public void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    transaction?.Dispose();
                    base.Dispose();
                }
            }
            _disposed = true;
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
