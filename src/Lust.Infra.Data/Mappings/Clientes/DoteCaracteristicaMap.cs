using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class DoteCaracteristicaMap : IEntityTypeConfiguration<DoteCaracteristica>
    {
        public void Configure(EntityTypeBuilder<DoteCaracteristica> builder)
        {

            builder.HasKey(q => new { q.CaracteristicaId, q.DoteId });

            builder.HasOne(pt => pt.Dote)
                .WithMany(p => p.Caracteristicas)
                .HasForeignKey(pt => pt.DoteId);

            builder.HasOne(pt => pt.Caracteristica)
                .WithMany(p => p.Dotes)
                .HasForeignKey(pt => pt.CaracteristicaId);

            builder.ToTable("DoteCaracteristica");
        }

    }
}
