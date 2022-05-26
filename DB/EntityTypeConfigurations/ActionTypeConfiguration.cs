using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DB.EntityTypeConfigurations
{
    class ActionTypeConfiguration : IEntityTypeConfiguration<ActionType>
    {
        public void Configure(EntityTypeBuilder<ActionType> builder)
        {
            builder
               .ToTable("ActionType", "public");

            builder
               .Property(e => e.ActionTypeId)
               .ValueGeneratedNever();
        }
    }
}