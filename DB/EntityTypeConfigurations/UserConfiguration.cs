using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.EntityTypeConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
               .ToTable("User", "public");

            builder
             .HasKey(e => e.UserId);

            builder
             .Property(e => e.UserId).ValueGeneratedOnAdd();

            builder
               .Property(e => e.FirstName)
               .HasMaxLength(20);
            
            builder
               .Property(e => e.LastName)
               .HasMaxLength(20);
            
            builder
               .Property(e => e.Email)
               .HasMaxLength(100);    
            
            builder
             .HasOne(e => e.Roles)
             .WithOne()
             .HasForeignKey<User>(d => d.RoleId);
        }
    }
}