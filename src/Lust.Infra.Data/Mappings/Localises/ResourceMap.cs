using Lust.Domain.Localises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Localises
{
    public class ResourceMap : BaseMap<Resource>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Resource> builder)
        {

            builder.Property(c => c.Key)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(c => c.Value)
                .HasColumnType("varchar(max)");

            builder.HasOne(c => c.Culture)
                 .WithMany(c => c.Resources)
                 .HasForeignKey(e => e.CultureId);

            builder.ToTable("Resource");
        }


    }
    
    
}
