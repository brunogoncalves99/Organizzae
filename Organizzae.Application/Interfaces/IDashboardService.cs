using Organizzae.Application.DTOs.Dashboard;

namespace Organizzae.Application.Interfaces
{
    public interface IDashboardService
    {
        Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim);
        Task<ResumoFinanceiroDto> ObterResumoMesAtualAsync(Guid usuarioId);
    }

}
