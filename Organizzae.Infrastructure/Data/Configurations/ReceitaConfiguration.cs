using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;
using Organizzae.Infrastructure.Data.Converters;

namespace Organizzae.Infrastructure.Data.Configurations;

public class ReceitaConfiguration : IEntityTypeConfiguration<Receita>
{
    public void Configure(EntityTypeBuilder<Receita> builder)
    {
        builder.ToTable("Receitas");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.UsuarioId)
            .IsRequired();

        builder.Property(r => r.CategoriaId)
            .IsRequired();

        builder.Property(r => r.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(r => r.Valor)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new DinheiroConverter());

        builder.Property(r => r.DataPrevista)
            .IsRequired();

        builder.Property(r => r.DataRecebimento);

        builder.Property(r => r.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.TipoRecorrencia)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(r => r.Observacoes)
            .HasMaxLength(500);

        builder.Property(r => r.DataCriacao)
            .IsRequired();

        builder.Property(r => r.DataAtualizacao);

        builder.Property(r => r.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(r => r.UsuarioId)
            .HasDatabaseName("IX_Receitas_UsuarioId");

        builder.HasIndex(r => r.CategoriaId)
            .HasDatabaseName("IX_Receitas_CategoriaId");

        builder.HasIndex(r => r.Status)
            .HasDatabaseName("IX_Receitas_Status");

        builder.HasIndex(r => r.DataPrevista)
            .HasDatabaseName("IX_Receitas_DataPrevista");

        builder.HasIndex(r => new { r.UsuarioId, r.DataPrevista })
            .HasDatabaseName("IX_Receitas_UsuarioId_DataPrevista");

        builder.HasIndex(r => r.TipoRecorrencia)
            .HasDatabaseName("IX_Receitas_TipoRecorrencia");

    }
}
