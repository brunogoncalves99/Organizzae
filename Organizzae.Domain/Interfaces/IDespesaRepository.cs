using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Interfaces
{
    public interface IDespesaRepository : IRepositoryBase<Despesa>
    {
        Task<IEnumerable<Despesa>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Despesa>> ObterPorUsuarioEPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Despesa>> ObterPorStatusAsync(Guid usuarioId, StatusDespesa status);
        Task<IEnumerable<Despesa>> ObterPorCategoriaAsync(Guid categoriaId);
        Task<IEnumerable<Despesa>> ObterVencimentoProximoAsync(Guid usuarioId, int proximosDias);
        Task<IEnumerable<Despesa>> ObterAtrasadasAsync(Guid usuarioId);
        Task<IEnumerable<Despesa>> ObterRecorrentesAsync(Guid usuarioId);
        Task<Dinheiro> CalcularTotalPorPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<Dinheiro> CalcularTotalPorCategoriaAsync(Guid usuarioId, Guid categoriaId, DateTime dataInicio, DateTime dataFim);
        Task<Dictionary<string, decimal>> ObterDespesasPorCategoriaAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Despesa>> ObterMaioresGastosAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim, int quantidade);
    }
}
