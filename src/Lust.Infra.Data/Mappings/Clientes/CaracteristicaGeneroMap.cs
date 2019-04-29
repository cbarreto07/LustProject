using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class CaracteristicaGeneroMap : IEntityTypeConfiguration<CaracteristicaGenero>
    {
        public void Configure(EntityTypeBuilder<CaracteristicaGenero> builder)
        {

            builder.HasKey(q => new { q.CaracteristicaId, q.Genero });

            builder.HasOne(pt => pt.Caracteristica)
                .WithMany(p => p.AtendeGeneros)
                .HasForeignKey(pt => pt.CaracteristicaId);

            builder.ToTable("CaracteristicaGenero");
        }

    }
}
