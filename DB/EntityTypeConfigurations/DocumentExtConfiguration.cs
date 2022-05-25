using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.EntityTypeConfigurations
{
    class DocumentExtConfiguration : IEntityTypeConfiguration<DocumentExt>
    {
        public void Configure(EntityTypeBuilder<DocumentExt> builder)
        {
            builder
               .ToTable("DocumentExt", "public");

            builder
             .Property(e => e.DocumentExtId)
             .ValueGeneratedOnAdd();

            builder.HasKey(e => e.DocumentExtId);

            builder
             .HasOne(d => d.Documents)
             .WithMany()
             .HasForeignKey(d => d.DocumentId)
             .HasConstraintName("FK_DocumentExt_Documents");

            builder
               .HasOne(d => d.Users)
               .WithMany()
               .HasForeignKey(d => d.UserId)
               .HasConstraintName("FK_DocumentExt_Users");

            builder
               .HasOne(d => d.ActionTypes)
               .WithMany()
               .HasForeignKey(d => d.ActionId)
               .HasConstraintName("FK_DocumentExt_ActionTypes");

            builder
               .HasOne(d => d.DocumentStatus)
               .WithMany()
               .HasForeignKey(d => d.DocumentStatusId)
               .HasConstraintName("FK_DocumentExt_DocumentStatus");

        }
    }
}