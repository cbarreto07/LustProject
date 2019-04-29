using Lust.Domain.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Assinaturas
{
    public class PagamentoMap : BaseMap<Pagamento>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Pagamento> builder)
        {


            builder.Property(c => c.Validade)
             .IsRequired()
             .HasColumnType("datetime")
             .IsRequired();

            builder.HasOne(c => c.Assinatura)
                  .WithMany(c => c.Pagamentos)
                  .HasForeignKey(e => e.AssinaturaId);

            builder.ToTable("Pagamento");
        }


    }
}
