using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Organizzae.Domain.Entities;
using Organizzae.Infrastructure.Data.Converters;

namespace Organizzae.Infrastructure.Data.Configurations;

public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuarios");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.CPF)
            .IsRequired()
            .HasMaxLength(11)
            .HasConversion(new CpfConverter());

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(200)
            .HasConversion(new EmailConverter());

        builder.Property(u => u.SenhaHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(u => u.FotoPerfil)
            .HasMaxLength(500);

        builder.Property(u => u.UltimoAcesso);

        builder.Property(u => u.DataCriacao)
            .IsRequired();

        builder.Property(u => u.DataAtualizacao);

        builder.Property(u => u.Ativo)
            .IsRequired()
            .HasDefaultValue(true);

        builder.HasIndex(u => u.CPF)
            .IsUnique()
            .HasDatabaseName("IX_Usuarios_Cpf");

        builder.HasIndex(u => u.Email)
            .IsUnique()
            .HasDatabaseName("IX_Usuarios_Email");

        builder.HasMany(u => u.Despesas)
            .WithOne(d => d.Usuario)
            .HasForeignKey(d => d.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Receitas)
            .WithOne(r => r.Usuario)
            .HasForeignKey(r => r.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(u => u.Objetivos)
            .WithOne(o => o.Usuario)
            .HasForeignKey(o => o.UsuarioId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
