using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;
using Organizzae.Infrastructure.Data.Converters;

namespace Organizzae.Infrastructure.Data.Configurations;

public class ObjetivoConfiguration : IEntityTypeConfiguration<Objetivo>
{
    public void Configure(EntityTypeBuilder<Objetivo> builder)
    {
        builder.ToTable("Objetivos");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.UsuarioId)
            .IsRequired();

        builder.Property(o => o.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(o => o.Descricao)
            .HasMaxLength(500);

        builder.Property(o => o.ValorTotal)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new DinheiroConverter());

        builder.Property(o => o.ValorEconomizado)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new DinheiroConverter());

        builder.Property(o => o.DataInicio)
            .IsRequired();

        builder.Property(o => o.DataAlvo)
            .IsRequired();

        builder.Property(o => o.DataConclusao);

        builder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(o => o.ImagemRepresentativa)
            .HasMaxLength(500);

        builder.Property(o => o.DataCriacao)
            .IsRequired();

        builder.Property(o => o.DataAtualizacao);

        builder.Property(o => o.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(o => o.UsuarioId)
            .HasDatabaseName("IX_Objetivos_UsuarioId");

        builder.HasIndex(o => o.Status)
            .HasDatabaseName("IX_Objetivos_Status");

        builder.HasIndex(o => o.DataAlvo)
            .HasDatabaseName("IX_Objetivos_DataAlvo");

        builder.HasIndex(o => new { o.UsuarioId, o.Status })
            .HasDatabaseName("IX_Objetivos_UsuarioId_Status");

    }
}
