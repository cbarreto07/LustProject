using Lust.Domain.Localises;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Localises
{
    public class CultureMap : BaseMap<Culture>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Culture> builder)
        {

            builder.Property(c => c.Name)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);


            builder.ToTable("Culture");
        }


    }
    
    
}
