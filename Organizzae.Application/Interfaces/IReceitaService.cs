using Organizzae.Application.DTOs.Receita;

namespace Organizzae.Application.Interfaces
{
    public interface IReceitaService
    {
        Task<IEnumerable<ReceitaDto>> ObterTodasReceitasPorUsuarioAsync(Guid usuarioId);
        Task<ReceitaDto?> ObterReceitaPorIdAsync(Guid id);
        Task<ReceitaDto> CriarReceitaAsync(CriarReceitaDto dto);
        Task AtualizarReceitaAsync(AtualizarReceitaDto dto);
        Task ExcluirReceitaAsync(Guid id);
        Task<IEnumerable<ReceitaDto>> ObterReceitasPorMesAnoAsync(Guid usuarioId, int mes, int ano);
        Task<decimal> ObterTotalReceitasPorMesAnoAsync(Guid usuarioId, int mes, int ano);
        Task<IEnumerable<ReceitaDto>> ObterReceitasRecorrentesAsync(Guid usuarioId);
        Task<decimal> ObterTotalReceitasRecorrentesAsync(Guid usuarioId);
    }
}
