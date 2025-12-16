using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;

namespace Organizzae.Infrastructure.Data.Configurations;

public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
{
    public void Configure(EntityTypeBuilder<Categoria> builder)
    {
        builder.ToTable("Categorias");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(c => c.Descricao)
            .HasMaxLength(200);

        builder.Property(c => c.Tipo)
            .IsRequired()
            .HasConversion<int>();

        builder.Property(c => c.Icone)
            .HasMaxLength(50);

        builder.Property(c => c.CorHexadecimal)
            .IsRequired()
            .HasMaxLength(7)
            .HasDefaultValue("#6c757d");

        builder.Property(c => c.EhPadrao)
            .IsRequired()
            .HasDefaultValue(false);

        builder.Property(c => c.DataCriacao)
            .IsRequired();

        builder.Property(c => c.DataAtualizacao);

        builder.Property(c => c.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(c => new { c.Nome, c.Tipo })
            .HasDatabaseName("IX_Categorias_Nome_Tipo");

        builder.HasIndex(c => c.Tipo)
            .HasDatabaseName("IX_Categorias_Tipo");

        builder.HasIndex(c => c.EhPadrao)
            .HasDatabaseName("IX_Categorias_EhPadrao");

        builder.HasMany(c => c.Despesas)
            .WithOne(d => d.Categoria)
            .HasForeignKey(d => d.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.Receitas)
            .WithOne(r => r.Categoria)
            .HasForeignKey(r => r.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
