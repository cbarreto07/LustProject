using Lust.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Infra.Data.Mappings.Chats
{
    public class ChatClienteMap : IEntityTypeConfiguration<ChatCliente>
    {
        public void Configure(EntityTypeBuilder<ChatCliente> builder)
        {

            builder.HasKey(t => new { t.ClienteId, t.ChatId });

            builder.HasOne(pt => pt.Cliente)
                .WithMany(p => p.Chats)
                .HasForeignKey(pt => pt.ClienteId);

            builder.HasOne(pt => pt.Chat)
                .WithMany(p => p.Clientes)
                .HasForeignKey(pt => pt.ChatId);

            builder.ToTable("ChatCliente");
        }
    }
}
