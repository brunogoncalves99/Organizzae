using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizzae.Application.DTOs.Receita;
using Organizzae.Application.Interfaces;
using System.Security.Claims;

namespace Organizzae.Web.Controllers
{
    [Authorize]
    public class ReceitasController : Controller
    {
        private readonly IReceitaService _receitaService;

        public ReceitasController(IReceitaService receitaService)
        {
            _receitaService = receitaService;
        }

        private Guid ObterUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdClaim!);
        }

        // GET: Receitas
        public async Task<IActionResult> Index(int? mes, int? ano)
        {
            try
            {
                var usuarioId = ObterUsuarioId();

                var mesAtual = mes ?? DateTime.Now.Month;
                var anoAtual = ano ?? DateTime.Now.Year;

                var receitas = await _receitaService.ObterReceitasPorMesAnoAsync(usuarioId, mesAtual, anoAtual);

                ViewBag.MesAtual = mesAtual;
                ViewBag.AnoAtual = anoAtual;
                ViewBag.TotalReceitas = receitas.Sum(r => r.Valor);

                return View(receitas);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar receitas: {ex.Message}";
                return View(new List<ReceitaDto>());
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CriarReceitaDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Por favor, corrija os erros no formulário.";
                    return View(dto);
                }

                dto.UsuarioId = ObterUsuarioId();
                await _receitaService.CriarReceitaAsync(dto);

                TempData["SuccessMessage"] = "Receita criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao criar receita: {ex.Message}";
                return View(dto);
            }
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            try
            {
                var receita = await _receitaService.ObterReceitaPorIdAsync(id);

                if (receita == null)
                {
                    TempData["ErrorMessage"] = "Receita não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                // Verificar se a receita pertence ao usuário
                var usuarioId = ObterUsuarioId();
                if (receita.UsuarioId != usuarioId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para editar esta receita.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = new AtualizarReceitaDto
                {
                    Id = receita.Id,
                    Descricao = receita.Descricao,
                    Valor = receita.Valor,
                    Data = receita.Data,
                    Recorrente = receita.Recorrente
                };

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar receita: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, AtualizarReceitaDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    TempData["ErrorMessage"] = "ID da receita não corresponde.";
                    return View(dto);
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Por favor, corrija os erros no formulário.";
                    return View(dto);
                }

                await _receitaService.AtualizarReceitaAsync(dto);

                TempData["SuccessMessage"] = "Receita atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao atualizar receita: {ex.Message}";
                return View(dto);
            }
        }
        public async Task<IActionResult> Detalhes(Guid id)
        {
            try
            {
                var receita = await _receitaService.ObterReceitaPorIdAsync(id);

                if (receita == null)
                {
                    TempData["ErrorMessage"] = "Receita não encontrada.";
                    return RedirectToAction(nameof(Index));
                }

                // Verificar se a receita pertence ao usuário
                var usuarioId = ObterUsuarioId();
                if (receita.UsuarioId != usuarioId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para visualizar esta receita.";
                    return RedirectToAction(nameof(Index));
                }

                return View(receita);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar receita: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                await _receitaService.ExcluirReceitaAsync(id);
                TempData["SuccessMessage"] = "Receita excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir receita: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Relatorio(int? ano)
        {
            try
            {
                var usuarioId = ObterUsuarioId();
                var anoAtual = ano ?? DateTime.Now.Year;

                var receitasPorMes = new Dictionary<int, decimal>();

                for (int mes = 1; mes <= 12; mes++)
                {
                    var receitas = await _receitaService.ObterReceitasPorMesAnoAsync(usuarioId, mes, anoAtual);
                    receitasPorMes[mes] = receitas.Sum(r => r.Valor);
                }

                ViewBag.AnoAtual = anoAtual;
                ViewBag.ReceitasPorMes = receitasPorMes;
                ViewBag.TotalAnual = receitasPorMes.Values.Sum();

                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao gerar relatório: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
