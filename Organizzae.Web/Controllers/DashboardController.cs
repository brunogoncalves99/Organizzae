using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizzae.Application.Interfaces;
using System.Security.Claims;

namespace Organizzae.Web.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = GetUsuarioId();
            var resumo = await _dashboardService.ObterResumoMesAtualAsync(usuarioId);

            return View(resumo);
        }

        [HttpGet]
        public async Task<IActionResult> Periodo(DateTime? dataInicio, DateTime? dataFim)
        {
            var usuarioId = GetUsuarioId();

            if (!dataInicio.HasValue || !dataFim.HasValue)
            {
                dataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dataFim = dataInicio.Value.AddMonths(1).AddDays(-1);
            }

            var resumo = await _dashboardService.ObterResumoFinanceiroAsync(
                usuarioId,
                dataInicio.Value,
                dataFim.Value
            );

            ViewData["DataInicio"] = dataInicio.Value.ToString("yyyy-MM-dd");
            ViewData["DataFim"] = dataFim.Value.ToString("yyyy-MM-dd");

            return View("Index", resumo);
        }

        private Guid GetUsuarioId()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(usuarioIdClaim ?? throw new UnauthorizedAccessException());
        }
    }

}
