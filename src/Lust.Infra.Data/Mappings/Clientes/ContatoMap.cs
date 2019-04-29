
using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class ContatoMap : IEntityTypeConfiguration<Contato>
    {
        public void Configure(EntityTypeBuilder<Contato> builder)
        {

            builder.HasKey(t => new { t.ClienteId, t.ContatoClienteId });

            builder.HasOne(pt => pt.Cliente)
                .WithMany(p => p.Contatos)
                .HasForeignKey(pt => pt.ClienteId);

            builder.HasOne(pt => pt.ContatoCliente)
                .WithMany(p => p.ContatoDeOutros)
                .HasForeignKey(pt => pt.ContatoClienteId);

            builder.ToTable("Contato");
        }
    }
}
