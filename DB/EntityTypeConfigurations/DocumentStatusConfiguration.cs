using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.EntityTypeConfigurations
{
    class DocumentStatusConfiguration : IEntityTypeConfiguration<DocumentStatus>
    {
        public void Configure(EntityTypeBuilder<DocumentStatus> builder)
        {
            builder
               .ToTable("DocumentStatus", "public");

            builder
               .Property(e => e.DocumentStatusId)
               .ValueGeneratedNever();

            builder.HasKey(e => e.DocumentStatusId);
        }
    }
}