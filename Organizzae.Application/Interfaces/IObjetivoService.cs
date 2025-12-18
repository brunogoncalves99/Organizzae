using Organizzae.Application.DTOs.Objetivo;

namespace Organizzae.Application.Interfaces
{
    public interface IObjetivoService
    {
        Task<IEnumerable<ObjetivoDto>> ObterTodosObjetivosPorUsuarioAsync(Guid usuarioId);
        Task<ObjetivoDto?> ObterObjetivoPorIdAsync(Guid id);
        Task<ObjetivoDto> CriarObjetivoAsync(CriarObjetivoDto dto);
        Task AtualizarObjetivoAsync(AtualizarObjetivoDto dto);
        Task ExcluirObjetivoAsync(Guid id);
        Task<IEnumerable<ObjetivoDto>> ObterObjetivosAtivosAsync(Guid usuarioId);
        Task<IEnumerable<ObjetivoDto>> ObterObjetivosConcluidosAsync(Guid usuarioId);
        Task<decimal> ObterTotalValorAlvoAsync(Guid usuarioId);
        Task<decimal> ObterTotalValorAtualAsync(Guid usuarioId);
    }
}
