using Lust.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Infra.Data.Mappings.Chats
{
    public class DialogoMap : BaseMap<Dialogo>
    {


        public override void ConfigureEntity(EntityTypeBuilder<Dialogo> builder)
        {
                        

            builder.Property(c => c.Mensagem)
               .HasColumnType("varchar(500)")
               .HasMaxLength(500);

            builder.HasOne(c => c.Cliente)
                  .WithMany(c => c.Dialogos)
                  .HasForeignKey(e => e.ClienteId);

            builder.HasOne(c => c.Chat)
                  .WithMany(c => c.Dialogos)
                  .HasForeignKey(e => e.ChatId);


            builder.ToTable("Dialogo");
        }
    }
}
