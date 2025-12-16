using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Interfaces
{
    public interface IReceitaRepository : IRepositoryBase<Receita>
    {
        Task<IEnumerable<Receita>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Receita>> ObterPorUsuarioEPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<IEnumerable<Receita>> ObterPorStatusAsync(Guid usuarioId, StatusReceita status);
        Task<IEnumerable<Receita>> ObterPorCategoriaAsync(Guid categoriaId);
        Task<IEnumerable<Receita>> ObterRecebimentoProximoAsync(Guid usuarioId, int proximosDias);
        Task<IEnumerable<Receita>> ObterAtrasadasAsync(Guid usuarioId);
        Task<IEnumerable<Receita>> ObterRecorrentesAsync(Guid usuarioId);
        Task<Dinheiro> CalcularTotalPorPeriodoAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<Dinheiro> CalcularTotalPorCategoriaAsync(Guid usuarioId, Guid categoriaId, DateTime dataInicio, DateTime dataFim);
        Task<Dictionary<string, decimal>> ObterReceitasPorCategoriaAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
    }
}
