using Lust.Domain.Sociais;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Sociais
{
    public class AvaliacaoMap : BaseMap<Avaliacao>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Avaliacao> builder)
        {
            
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);
                

            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.HasOne(c => c.ClienteAvaliador)
                  .WithMany(c => c.AvaliacoesFeitas)
                  .HasForeignKey(e => e.ClienteAvaliadorId);

            builder.HasOne(c => c.ClienteAvaliado)
                  .WithMany(c => c.AvaliacoesRecebidas)
                  .HasForeignKey(e => e.ClienteAvaliadoId);

            builder.ToTable("Avaliacao");
        }


    }
}
