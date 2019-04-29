using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class PreferenciaGeneroMap : IEntityTypeConfiguration<PreferenciaGenero>
    {
        public void Configure(EntityTypeBuilder<PreferenciaGenero> builder)
        {

            builder.HasKey(q => new { q.PreferenciaId, q.Genero });

            builder.HasOne(pt => pt.Preferencia)
                .WithMany(p => p.PrefereGeneros)
                .HasForeignKey(pt => pt.PreferenciaId);

            builder.ToTable("PreferenciaGenero");
        }

    }
}
