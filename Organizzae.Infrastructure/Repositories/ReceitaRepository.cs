using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories;

public class ReceitaRepository : RepositoryBase<Receita>, IReceitaRepository
{
    public ReceitaRepository(OrganizzaeDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Receita>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId && r.Ativo)
            .OrderByDescending(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterPorUsuarioEPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.Ativo
                     && r.DataPrevista.Date >= dataInicio.Date
                     && r.DataPrevista.Date <= dataFim.Date)
            .OrderBy(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterPorStatusAsync(Guid usuarioId, StatusReceita status)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.Status == status
                     && r.Ativo)
            .OrderBy(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterPorCategoriaAsync(Guid categoriaId)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.CategoriaId == categoriaId && r.Ativo)
            .OrderByDescending(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterRecebimentoProximoAsync(Guid usuarioId, int proximosDias)
    {
        var dataLimite = DateTime.Now.Date.AddDays(proximosDias);

        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.Status == StatusReceita.Pendente
                     && r.Ativo
                     && r.DataPrevista.Date >= DateTime.Now.Date
                     && r.DataPrevista.Date <= dataLimite)
            .OrderBy(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterAtrasadasAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.Status == StatusReceita.Pendente
                     && r.DataPrevista.Date < DateTime.Now.Date
                     && r.Ativo)
            .OrderBy(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<IEnumerable<Receita>> ObterRecorrentesAsync(Guid usuarioId)
    {
        return await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.TipoRecorrencia != TipoRecorrencia.Nenhuma
                     && r.Ativo)
            .OrderBy(r => r.DataPrevista)
            .ToListAsync();
    }

    public async Task<Dinheiro> CalcularTotalPorPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        var receitas = await _dbSet
            .Where(r => r.UsuarioId == usuarioId
                     && r.Status == StatusReceita.Recebida
                     && r.DataRecebimento.HasValue
                     && r.DataRecebimento.Value.Date >= dataInicio.Date
                     && r.DataRecebimento.Value.Date <= dataFim.Date
                     && r.Ativo)
            .ToListAsync();

        var total = receitas.Sum(r => r.Valor.Valor);
        return new Dinheiro(total);
    }

    public async Task<Dinheiro> CalcularTotalPorCategoriaAsync(Guid usuarioId, Guid categoriaId, DateTime dataInicio, DateTime dataFim)
    {
        var receitas = await _dbSet
            .Where(r => r.UsuarioId == usuarioId
                     && r.CategoriaId == categoriaId
                     && r.Status == StatusReceita.Recebida
                     && r.DataRecebimento.HasValue
                     && r.DataRecebimento.Value.Date >= dataInicio.Date
                     && r.DataRecebimento.Value.Date <= dataFim.Date
                     && r.Ativo)
            .ToListAsync();

        var total = receitas.Sum(r => r.Valor.Valor);
        return new Dinheiro(total);
    }

    public async Task<Dictionary<string, decimal>> ObterReceitasPorCategoriaAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
    {
        var receitas = await _dbSet
            .Include(r => r.Categoria)
            .Where(r => r.UsuarioId == usuarioId
                     && r.Status == StatusReceita.Recebida
                     && r.DataRecebimento.HasValue
                     && r.DataRecebimento.Value.Date >= dataInicio.Date
                     && r.DataRecebimento.Value.Date <= dataFim.Date
                     && r.Ativo)
            .ToListAsync();

        return receitas
            .GroupBy(r => r.Categoria.Nome)
            .ToDictionary(g => g.Key, g => g.Sum(r => r.Valor.Valor));
    }
}
