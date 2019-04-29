using Lust.Domain.Sociais;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Sociais
{
    public class ContactUsMap : BaseMap<ContactUs>
    {
        public override void ConfigureEntity(EntityTypeBuilder<ContactUs> builder)
        {
            
            builder.Property(c => c.Name)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255);
                

            builder.Property(c => c.Email)
                .HasColumnType("varchar(255)")
                .HasMaxLength(255)
                .IsRequired();

            builder.Property(c => c.Message)
               .HasColumnType("varchar(1024)")
               .HasMaxLength(1024)
               .IsRequired();

          

            builder.ToTable("ContactUs");
        }


    }
}
