using Lust.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lust.Infra.Data.Mappings.Chats
{
    public class DialogoLeituraMap : BaseMap<DialogoLeitura>
    {
        
        public override void ConfigureEntity(EntityTypeBuilder<DialogoLeitura> builder)
        {
           
            builder.HasOne(c => c.Cliente)
                  .WithMany(c => c.DialogoLeituras)
                  .HasForeignKey(e => e.ClienteId);

            builder.HasOne(c => c.Dialogo)
                  .WithMany(c => c.DialogoLeituras)
                  .HasForeignKey(e => e.DialogoId);


            builder.ToTable("DialogoLeitura");
        }
    }
}
