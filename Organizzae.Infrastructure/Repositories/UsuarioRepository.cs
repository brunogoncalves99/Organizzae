using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories;

public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(OrganizzaeDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> ObterPorCpfAsync(CPF cpf)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.CPF == cpf);
    }

    public async Task<Usuario?> ObterPorEmailAsync(Email email)
    {
        return await _dbSet
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<bool> CpfExisteAsync(CPF cpf)
    {
        return await _dbSet
            .AnyAsync(u => u.CPF == cpf);
    }

    public async Task<bool> EmailExisteAsync(Email email)
    {
        return await _dbSet
            .AnyAsync(u => u.Email == email);
    }

    public async Task<Usuario?> ObterComDespesasAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(u => u.Despesas)
            .ThenInclude(d => d.Categoria)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
    }

    public async Task<Usuario?> ObterComReceitasAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(u => u.Receitas)
            .ThenInclude(r => r.Categoria)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
    }

    public async Task<Usuario?> ObterComObjetivosAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(u => u.Objetivos)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
    }

    public async Task<Usuario?> ObterCompletoAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(u => u.Despesas)
            .ThenInclude(d => d.Categoria)
            .Include(u => u.Receitas)
            .ThenInclude(r => r.Categoria)
            .Include(u => u.Objetivos)
            .FirstOrDefaultAsync(u => u.Id == usuarioId);
    }
}
