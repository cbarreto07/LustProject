using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class DoteMap : BaseMap<Dote>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Dote> builder)
        {

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(50)")
                .HasMaxLength(50)
                .IsRequired();

            builder.ToTable("Dote");
        }
    }
}
