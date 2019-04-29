using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class FotoMap : BaseMap<Foto>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Foto> builder)
        {
                   
                

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(c => c.MotivoReprovado)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.HasOne(c => c.Cliente)
                  .WithMany(c => c.Fotos)
                  .HasForeignKey(e => e.ClienteId);


            builder.ToTable("Foto");
        }


    }
}
