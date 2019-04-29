using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Lust.Infra.Data.Mappings.Clientes
{
    public class CaracteristicaMap : IEntityTypeConfiguration<Caracteristica>
    {
        public void Configure(EntityTypeBuilder<Caracteristica> builder)
        {
            
            

            builder.Property(c => c.Valor30min).IsRequired();
            builder.Property(c => c.Valor1Hora).IsRequired();
            builder.Property(c => c.Valor2horas).IsRequired();
            builder.Property(c => c.ValorPernoite).IsRequired();


            

            builder.ToTable("Caracteristica");
        }

    }
}
