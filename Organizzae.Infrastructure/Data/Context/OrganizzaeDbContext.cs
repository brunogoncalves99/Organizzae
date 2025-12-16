using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Infrastructure.Data.Configurations;
using Organizzae.Infrastructure.Data.Seed;

namespace Organizzae.Infrastructure.Data.Context
{
    public class OrganizzaeDbContext : DbContext
    {
        public OrganizzaeDbContext(DbContextOptions<OrganizzaeDbContext> options)
        : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Despesa> Despesas { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Objetivo> Objetivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new UsuarioConfiguration());
            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new DespesaConfiguration());
            modelBuilder.ApplyConfiguration(new ReceitaConfiguration());
            modelBuilder.ApplyConfiguration(new ObjetivoConfiguration());

            modelBuilder.ApplyConfiguration(new CategoriaSeed());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {


            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                if (entry.Entity.GetType().GetProperty("DataAtualizacao") != null)
                {
                    entry.Property("DataAtualizacao").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
