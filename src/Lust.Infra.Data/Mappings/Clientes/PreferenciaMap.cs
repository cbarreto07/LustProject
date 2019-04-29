using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Lust.Infra.Data.Mappings.Clientes
{
    public class PreferenciaMap : IEntityTypeConfiguration<Preferencia>
    {
        public void Configure(EntityTypeBuilder<Preferencia> builder)
        {
            

            builder.Property(c => c.Distancia).IsRequired();
            builder.Property(c => c.IdadeMinima).IsRequired();
            builder.Property(c => c.IdadeMaxima).IsRequired();
            builder.Property(c => c.PrecoMinimo).IsRequired();
            builder.Property(c => c.PrecoMaximo).IsRequired();

            builder.ToTable("Preferencia");
        }

    }
}
