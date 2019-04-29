using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class VideoMap : BaseMap<Video>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Video> builder)
        {
            
            builder.Property(c => c.Descricao)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.Property(c => c.MotivoReprovado)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);

            builder.HasOne(c => c.Cliente)
                  .WithMany(c => c.Videos)
                  .HasForeignKey(e => e.ClienteId);


            builder.ToTable("Video");
        }


    }
}
