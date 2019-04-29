using Lust.Domain.Planos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Planos
{
    public class PlanoMap : BaseMap<Plano>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Plano> builder)
        {
            

            builder.Property(c => c.Titulo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.SubTitulo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Ordem)               
               .IsRequired();

            builder.Property(c => c.Destinado)
               .IsRequired();

            builder.Property(c => c.QuantidadeMeses)
               .IsRequired();

            builder.Property(c => c.Valor)
               .IsRequired();


            builder.ToTable("Plano");
        }


    }
}

