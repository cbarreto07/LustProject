using Lust.Domain.Clientes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Lust.Infra.Data.Mappings.Clientes
{
    public class ClienteMap : BaseMap<Cliente>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Cliente> builder)
        {            

            builder.Property(c => c.Nome)
                .HasColumnType("varchar(250)")
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(c => c.Email)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Celular)
               .HasColumnType("varchar(15)")
               .HasMaxLength(15)
               .IsRequired();

            builder.Property(c => c.Cpf)
               .HasColumnType("varchar(11)")
               .HasMaxLength(11)
               .IsRequired();

            builder.Property(c => c.DataNascimento)
                .IsRequired()
                .HasColumnType("datetime");

            builder.Property(c => c.Cep)
               .HasColumnType("varchar(10)")
               .HasMaxLength(10)
               .IsRequired();

            builder.Property(c => c.FotoDeCapa)
               .HasColumnName("FotoDeCapa");

            builder.Property(c => c.FotoDePerfil)
               .HasColumnName("FotoDePerfil");


            builder.Property(c => c.CurtaDescricao)
               .HasColumnType("varchar(255)")
               .HasMaxLength(255)
               .IsRequired();

            builder.Property(c => c.LongaDescricao)
               .HasColumnType("varchar(5000)")
               .HasMaxLength(5000)
               .IsRequired();

            builder.HasOne(q => q.Caracteristica)
                .WithOne(q => q.Cliente)
                .HasForeignKey<Caracteristica>(q => q.Id);

            builder.HasOne(q => q.Preferencia)
                .WithOne(q => q.Cliente)
                .HasForeignKey<Preferencia>(q => q.Id);

            builder.ToTable("Cliente");
        }

        
    }
}
