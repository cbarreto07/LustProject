
using Lust.Domain.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public abstract class BaseMap<T> : IEntityTypeConfiguration<T> where T : Entity
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.DataHoraCriacao)
            .IsRequired()
            .HasColumnType("datetime");
            

        builder.Property(c => c.DataHoraAlteracao)            
            .HasColumnType("datetime");

        ConfigureEntity(builder);
    }

    public abstract void ConfigureEntity(EntityTypeBuilder<T> builder);


}