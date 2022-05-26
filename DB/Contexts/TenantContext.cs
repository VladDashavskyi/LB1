using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using DB.Entities;
using DB.EntityTypeConfigurations;
using DB.Interfaces;

namespace DB.Contexts
{
    public class TenantContext : DbContext, ITenantContext
    {
        private          IDbContextTransaction _transaction;

        public DbSet<User>                 Users                 { get; set; }
        public DbSet<ActionType>           ActionTypes           { get; set; }
        public DbSet<Role>                 Roles                 { get; set; }
        public DbSet<Documents>            Documents             { get; set; }  
        public DbSet<DocumentExt>          DocumentExts          { get; set; }  
        public DbSet<DocumentStatus>       DocumentStatus        { get; set; }  

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseNpgsql("Host=localhost;Database=Advertisement;Username=postgres;Password=111");

        public void BeginTransaction()
        {
            _transaction = Database.BeginTransaction();
        }

        public bool Commit()
        {
            int resultCount;

            try
            {
                resultCount = SaveChanges();
                _transaction?.Commit();
            }
            finally
            {
                _transaction?.Dispose();
            }

            return resultCount > 0;
        }

        public void Rollback()
        {
            _transaction?.Rollback();
            _transaction?.Dispose();
        }

        public async Task<bool> SaveChangesAsync()
        {
            int changes = ChangeTracker
                         .Entries()
                         .Count(p => p.State == EntityState.Modified
                                  || p.State == EntityState.Deleted
                                  || p.State == EntityState.Added);

            if (changes == 0) return true;

            return await base.SaveChangesAsync() > 0;
        }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());           
            modelBuilder.ApplyConfiguration(new ActionTypeConfiguration());           
            modelBuilder.ApplyConfiguration(new RoleConfiguration());   
            modelBuilder.ApplyConfiguration(new DocumentsConfiguration());               
            modelBuilder.ApplyConfiguration(new DocumentExtConfiguration());               
        }
    }
}