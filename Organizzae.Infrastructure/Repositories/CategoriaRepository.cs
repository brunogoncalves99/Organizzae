using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories;

public class CategoriaRepository : RepositoryBase<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(OrganizzaeDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Categoria>> ObterPorTipoAsync(TipoCategoria tipo)
    {
        return await _dbSet
            .Where(c => c.Tipo == tipo && c.Ativo)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategoriasParaDespesaAsync()
    {
        return await _dbSet
            .Where(c => (c.Tipo == TipoCategoria.Despesa || c.Tipo == TipoCategoria.Ambos) && c.Ativo)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategoriasParaReceitaAsync()
    {
        return await _dbSet
            .Where(c => (c.Tipo == TipoCategoria.Receita || c.Tipo == TipoCategoria.Ambos) && c.Ativo)
            .OrderBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategoriasPadraoAsync()
    {
        return await _dbSet
            .Where(c => c.EhPadrao && c.Ativo)
            .OrderBy(c => c.Tipo)
            .ThenBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<IEnumerable<Categoria>> ObterCategoriasPersonalizadasAsync()
    {
        return await _dbSet
            .Where(c => !c.EhPadrao && c.Ativo)
            .OrderBy(c => c.Tipo)
            .ThenBy(c => c.Nome)
            .ToListAsync();
    }

    public async Task<bool> NomeExisteAsync(string nome, TipoCategoria tipo)
    {
        return await _dbSet
            .AnyAsync(c => c.Nome.ToLower() == nome.ToLower()
                        && c.Tipo == tipo
                        && c.Ativo);
    }

    public async Task<Categoria?> ObterPorNomeAsync(string nome)
    {
        return await _dbSet
            .FirstOrDefaultAsync(c => c.Nome.ToLower() == nome.ToLower() && c.Ativo);
    }
}

