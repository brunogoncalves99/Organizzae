using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;
using Organizzae.Infrastructure.Data.Converters;

namespace Organizzae.Infrastructure.Data.Configurations;

public class DespesaConfiguration : IEntityTypeConfiguration<Despesa>
{
    public void Configure(EntityTypeBuilder<Despesa> builder)
    {
        builder.ToTable("Despesas");

        builder.HasKey(d => d.Id);

        builder.Property(d => d.UsuarioId)
            .IsRequired();

        builder.Property(d => d.CategoriaId)
            .IsRequired();

        builder.Property(d => d.Descricao)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(d => d.Valor)
            .IsRequired()
            .HasPrecision(18, 2)
            .HasConversion(new DinheiroConverter());

        builder.Property(d => d.DataVencimento)
            .IsRequired();

        builder.Property(d => d.DataPagamento);

        builder.Property(d => d.Status)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(d => d.FormaPagamento)
            .HasConversion<int>();

        builder.Property(d => d.TipoRecorrencia)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(d => d.Observacoes)
            .HasMaxLength(500);

        builder.Property(d => d.AnexoComprovante)
            .HasMaxLength(500);

        builder.Property(d => d.DataCriacao)
            .IsRequired();

        builder.Property(d => d.DataAtualizacao);

        builder.Property(d => d.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        // Índices
        builder.HasIndex(d => d.UsuarioId)
            .HasDatabaseName("IX_Despesas_UsuarioId");

        builder.HasIndex(d => d.CategoriaId)
            .HasDatabaseName("IX_Despesas_CategoriaId");

        builder.HasIndex(d => d.Status)
            .HasDatabaseName("IX_Despesas_Status");

        builder.HasIndex(d => d.DataVencimento)
            .HasDatabaseName("IX_Despesas_DataVencimento");

        builder.HasIndex(d => new { d.UsuarioId, d.DataVencimento })
            .HasDatabaseName("IX_Despesas_UsuarioId_DataVencimento");

        builder.HasIndex(d => d.TipoRecorrencia)
            .HasDatabaseName("IX_Despesas_TipoRecorrencia");

    }
}