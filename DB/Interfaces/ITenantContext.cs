using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DB.Interfaces
{
    public interface ITenantContext
    {
        DbSet<User> Users { get; set; }
        DbSet<ActionType> ActionTypes { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<Documents> Documents { get; set; }
        DbSet<DocumentExt> DocumentExts { get; set; }
        DbSet<DocumentStatus> DocumentStatus { get; set; }
        

        void BeginTransaction();
        bool Commit();
        void Rollback();
        Task<bool> SaveChangesAsync();
    }
}
