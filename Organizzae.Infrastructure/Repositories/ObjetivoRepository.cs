using Microsoft.EntityFrameworkCore;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories
{
    public class ObjetivoRepository : RepositoryBase<Objetivo>, IObjetivoRepository
    {
        public ObjetivoRepository(OrganizzaeDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Objetivo>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId && o.Ativo)
                .OrderByDescending(o => o.DataCriacao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Objetivo>> ObterPorStatusAsync(Guid usuarioId, StatusObjetivo status)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == status
                         && o.Ativo)
                .OrderBy(o => o.DataAlvo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Objetivo>> ObterEmAndamentoAsync(Guid usuarioId)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == StatusObjetivo.EmAndamento
                         && o.Ativo)
                .OrderBy(o => o.DataAlvo)
                .ToListAsync();
        }

        public async Task<IEnumerable<Objetivo>> ObterAlcancadosAsync(Guid usuarioId)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == StatusObjetivo.Alcancado
                         && o.Ativo)
                .OrderByDescending(o => o.DataConclusao)
                .ToListAsync();
        }

        public async Task<IEnumerable<Objetivo>> ObterProximosDeAlcancarAsync(Guid usuarioId, decimal percentualMinimo)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == StatusObjetivo.EmAndamento
                         && o.Ativo)
                .Where(o => o.CalcularPercentualProgresso() >= percentualMinimo)
                .OrderByDescending(o => o.CalcularPercentualProgresso())
                .ToListAsync();
        }

        public async Task<IEnumerable<Objetivo>> ObterComPrazoProximoAsync(Guid usuarioId, int proximosDias)
        {
            var dataLimite = DateTime.Now.Date.AddDays(proximosDias);

            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == StatusObjetivo.EmAndamento
                         && o.Ativo
                         && o.DataAlvo.Date >= DateTime.Now.Date
                         && o.DataAlvo.Date <= dataLimite)
                .OrderBy(o => o.DataAlvo)
                .ToListAsync();
        }

        public async Task<Dinheiro> CalcularTotalEconomizadoAsync(Guid usuarioId)
        {
            var total = await _dbSet
                .Where(o => o.UsuarioId == usuarioId && o.Ativo)
                .SumAsync(o => o.ValorEconomizado.Valor);

            return new Dinheiro(total);
        }

        public async Task<Dinheiro> CalcularTotalObjetivosAsync(Guid usuarioId)
        {
            var total = await _dbSet
                .Where(o => o.UsuarioId == usuarioId
                         && o.Status == StatusObjetivo.EmAndamento
                         && o.Ativo)
                .SumAsync(o => o.ValorTotal.Valor);

            return new Dinheiro(total);
        }

        public async Task<int> ContarObjetivosAlcancadosAsync(Guid usuarioId)
        {
            return await _dbSet
                .CountAsync(o => o.UsuarioId == usuarioId
                              && o.Status == StatusObjetivo.Alcancado
                              && o.Ativo);
        }
    }
}
