using Lust.Domain.Assinaturas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Assinaturas
{
    public class AssinaturaMap : BaseMap<Assinatura>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Assinatura> builder)
        {

            builder.Property(c => c.PreApprovalCode)
               .HasColumnType("varchar(255)")
               .HasMaxLength(255);
               
               

            builder.HasOne(c => c.Cliente)
                  .WithMany(c => c.Assinaturas)
                  .HasForeignKey(e => e.ClienteId);

            builder.HasOne(c => c.Plano)
                 .WithMany(c => c.Assinaturas)
                 .HasForeignKey(e => e.PlanoId);


            builder.ToTable("Assinatura");
        }


    }
}
