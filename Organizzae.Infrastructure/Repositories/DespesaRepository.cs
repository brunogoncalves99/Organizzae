using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories
{
    public class DespesaRepository : RepositoryBase<Despesa>, IDespesaRepository
    {
        public DespesaRepository(OrganizzaeDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Despesa>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId && d.Ativo)
                .OrderByDescending(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterPorUsuarioEPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.Ativo
                         && d.DataVencimento.Date >= dataInicio.Date
                         && d.DataVencimento.Date <= dataFim.Date)
                .OrderBy(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterPorStatusAsync(Guid usuarioId, StatusDespesa status)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.Status == status
                         && d.Ativo)
                .OrderBy(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterPorCategoriaAsync(Guid categoriaId)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.CategoriaId == categoriaId && d.Ativo)
                .OrderByDescending(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterVencimentoProximoAsync(Guid usuarioId, int proximosDias)
        {
            var dataLimite = DateTime.Now.Date.AddDays(proximosDias);

            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.Status == StatusDespesa.Pendente
                         && d.Ativo
                         && d.DataVencimento.Date >= DateTime.Now.Date
                         && d.DataVencimento.Date <= dataLimite)
                .OrderBy(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterAtrasadasAsync(Guid usuarioId)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && (d.Status == StatusDespesa.Atrasada ||
                             (d.Status == StatusDespesa.Pendente && d.DataVencimento.Date < DateTime.Now.Date))
                         && d.Ativo)
                .OrderBy(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<IEnumerable<Despesa>> ObterRecorrentesAsync(Guid usuarioId)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.TipoRecorrencia != TipoRecorrencia.Nenhuma
                         && d.Ativo)
                .OrderBy(d => d.DataVencimento)
                .ToListAsync();
        }

        public async Task<Dinheiro> CalcularTotalPorPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            var total = await _dbSet
                .Where(d => d.UsuarioId == usuarioId
                         && d.Status == StatusDespesa.Paga
                         && d.DataPagamento.HasValue
                         && d.DataPagamento.Value.Date >= dataInicio.Date
                         && d.DataPagamento.Value.Date <= dataFim.Date
                         && d.Ativo)
                .SumAsync(d => d.Valor.Valor);

            return new Dinheiro(total);
        }

        public async Task<Dinheiro> CalcularTotalPorCategoriaAsync(Guid usuarioId, Guid categoriaId, DateTime dataInicio, DateTime dataFim)
        {
            var total = await _dbSet
                .Where(d => d.UsuarioId == usuarioId
                         && d.CategoriaId == categoriaId
                         && d.Status == StatusDespesa.Paga
                         && d.DataPagamento.HasValue
                         && d.DataPagamento.Value.Date >= dataInicio.Date
                         && d.DataPagamento.Value.Date <= dataFim.Date
                         && d.Ativo)
                .SumAsync(d => d.Valor.Valor);

            return new Dinheiro(total);
        }

        public async Task<Dictionary<string, decimal>> ObterDespesasPorCategoriaAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.Status == StatusDespesa.Paga
                         && d.DataPagamento.HasValue
                         && d.DataPagamento.Value.Date >= dataInicio.Date
                         && d.DataPagamento.Value.Date <= dataFim.Date
                         && d.Ativo)
                .GroupBy(d => d.Categoria.Nome)
                .Select(g => new { Categoria = g.Key, Total = g.Sum(d => d.Valor.Valor) })
                .ToDictionaryAsync(x => x.Categoria, x => x.Total);
        }

        public async Task<IEnumerable<Despesa>> ObterMaioresGastosAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim, int quantidade)
        {
            return await _dbSet
                .Include(d => d.Categoria)
                .Where(d => d.UsuarioId == usuarioId
                         && d.Status == StatusDespesa.Paga
                         && d.DataPagamento.HasValue
                         && d.DataPagamento.Value.Date >= dataInicio.Date
                         && d.DataPagamento.Value.Date <= dataFim.Date
                         && d.Ativo)
                .OrderByDescending(d => d.Valor.Valor)
                .Take(quantidade)
                .ToListAsync();
        }
    }
}
