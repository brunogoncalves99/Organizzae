using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Interfaces
{
    public interface IObjetivoRepository : IRepositoryBase<Objetivo>
    {
        Task<IEnumerable<Objetivo>> ObterPorUsuarioAsync(Guid usuarioId);
        Task<IEnumerable<Objetivo>> ObterPorStatusAsync(Guid usuarioId, StatusObjetivo status);
        Task<IEnumerable<Objetivo>> ObterEmAndamentoAsync(Guid usuarioId);
        Task<IEnumerable<Objetivo>> ObterAlcancadosAsync(Guid usuarioId);
        Task<IEnumerable<Objetivo>> ObterProximosDeAlcancarAsync(Guid usuarioId, decimal percentualMinimo);
        Task<IEnumerable<Objetivo>> ObterComPrazoProximoAsync(Guid usuarioId, int proximosDias);
        Task<Dinheiro> CalcularTotalEconomizadoAsync(Guid usuarioId);
        Task<Dinheiro> CalcularTotalObjetivosAsync(Guid usuarioId);
        Task<int> ContarObjetivosAlcancadosAsync(Guid usuarioId);
    }
}
