using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.EntityTypeConfigurations
{
    class DocumentsConfiguration : IEntityTypeConfiguration<Documents>
    {
        public void Configure(EntityTypeBuilder<Documents> builder)
        {
            builder
               .ToTable("Documents", "public");

            builder
             .HasKey(e => e.DocumentId);

            builder
             .Property(e => e.DocumentId).ValueGeneratedOnAdd();           
             
        }
    }
}