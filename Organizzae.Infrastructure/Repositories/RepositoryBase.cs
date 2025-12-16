using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Interfaces;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories;

public class RepositoryBase<T> : IRepositoryBase<T> where T : EntidadeBase
{
    protected readonly OrganizzaeDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public RepositoryBase(OrganizzaeDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.Where(e => e.Ativo).ToListAsync();
    }

    public virtual async Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public virtual async Task<T?> BuscarUnicoAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public virtual async Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public virtual async Task<int> ContarAsync(Expression<Func<T, bool>>? predicate = null)
    {
        if (predicate == null)
            return await _dbSet.CountAsync();

        return await _dbSet.CountAsync(predicate);
    }

    public virtual async Task AdicionarAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public virtual async Task AdicionarVariosAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual void Atualizar(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual void AtualizarVarios(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
    }

    public virtual void Remover(T entity)
    {
        _dbSet.Remove(entity);
    }

    public virtual void RemoverVarios(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public virtual async Task<IEnumerable<T>> ObterPaginadoAsync(
        int pagina,
        int tamanhoPagina,
        Expression<Func<T, bool>>? filtro = null,
        Expression<Func<T, object>>? ordenarPor = null,
        bool ordenarDecrescente = false)
    {
        IQueryable<T> query = _dbSet;

        if (filtro != null)
            query = query.Where(filtro);

        if (ordenarPor != null)
        {
            query = ordenarDecrescente
                ? query.OrderByDescending(ordenarPor)
                : query.OrderBy(ordenarPor);
        }

        return await query
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .ToListAsync();
    }
}
