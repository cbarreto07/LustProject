using Lust.Domain.Chats;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;


namespace Lust.Infra.Data.Mappings.Chats
{
    public class ChatMap : BaseMap<Chat>
    {

        public override void ConfigureEntity(EntityTypeBuilder<Chat> builder)
        {

            builder.Property(c => c.Nome)
               .HasColumnType("varchar(255)")
               .HasMaxLength(255);

            builder.ToTable("Chat");
        }
    }
}