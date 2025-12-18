using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organizzae.Application.DTOs.Objetivo;
using Organizzae.Application.Interfaces;
using System.Security.Claims;

namespace Organizzae.Web.Controllers
{
    [Authorize]
    public class ObjetivosController : Controller
    {
        private readonly IObjetivoService _objetivoService;

        public ObjetivosController(IObjetivoService objetivoService)
        {
            _objetivoService = objetivoService;
        }

        private Guid ObterUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(userIdClaim!);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var usuarioId = ObterUsuarioId();
                var objetivos = await _objetivoService.ObterTodosObjetivosPorUsuarioAsync(usuarioId);
                return View(objetivos);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar objetivos: {ex.Message}";
                return View(new List<ObjetivoDto>());
            }
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CriarObjetivoDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Por favor, corrija os erros no formulário.";
                    return View(dto);
                }

                dto.UsuarioId = ObterUsuarioId();
                await _objetivoService.CriarObjetivoAsync(dto);

                TempData["SuccessMessage"] = "Objetivo criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao criar objetivo: {ex.Message}";
                return View(dto);
            }
        }

        public async Task<IActionResult> Editar(Guid id)
        {
            try
            {
                var objetivo = await _objetivoService.ObterObjetivoPorIdAsync(id);

                if (objetivo == null)
                {
                    TempData["ErrorMessage"] = "Objetivo não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                var usuarioId = ObterUsuarioId();
                if (objetivo.UsuarioId != usuarioId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para editar este objetivo.";
                    return RedirectToAction(nameof(Index));
                }

                var dto = new AtualizarObjetivoDto
                {
                    Id = objetivo.Id,
                    Descricao = objetivo.Descricao,
                    ValorAlvo = objetivo.ValorAlvo,
                    DataInicio = objetivo.DataInicio,
                };

                return View(dto);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar objetivo: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Guid id, AtualizarObjetivoDto dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    TempData["ErrorMessage"] = "ID do objetivo não corresponde.";
                    return View(dto);
                }

                if (!ModelState.IsValid)
                {
                    TempData["ErrorMessage"] = "Por favor, corrija os erros no formulário.";
                    return View(dto);
                }

                await _objetivoService.AtualizarObjetivoAsync(dto);

                TempData["SuccessMessage"] = "Objetivo atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao atualizar objetivo: {ex.Message}";
                return View(dto);
            }
        }

        public async Task<IActionResult> Detalhes(Guid id)
        {
            try
            {
                var objetivo = await _objetivoService.ObterObjetivoPorIdAsync(id);

                if (objetivo == null)
                {
                    TempData["ErrorMessage"] = "Objetivo não encontrado.";
                    return RedirectToAction(nameof(Index));
                }

                // Verificar se o objetivo pertence ao usuário
                var usuarioId = ObterUsuarioId();
                if (objetivo.UsuarioId != usuarioId)
                {
                    TempData["ErrorMessage"] = "Você não tem permissão para visualizar este objetivo.";
                    return RedirectToAction(nameof(Index));
                }

                return View(objetivo);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao carregar objetivo: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                await _objetivoService.ExcluirObjetivoAsync(id);
                TempData["SuccessMessage"] = "Objetivo excluído com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir objetivo: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarcarConcluido(Guid id)
        {
            try
            {
                var objetivo = await _objetivoService.ObterObjetivoPorIdAsync(id);

                if (objetivo == null)
                {
                    return Json(new { success = false, message = "Objetivo não encontrado." });
                }

                var dto = new AtualizarObjetivoDto
                {
                    Id = objetivo.Id,
                    Descricao = objetivo.Descricao,
                    ValorAlvo = objetivo.ValorAlvo,
                    DataInicio = objetivo.DataInicio
                };

                await _objetivoService.AtualizarObjetivoAsync(dto);

                return Json(new { success = true, message = "Status atualizado com sucesso!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao atualizar status: {ex.Message}" });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarValor(Guid id, decimal valor)
        {
            try
            {
                var objetivo = await _objetivoService.ObterObjetivoPorIdAsync(id);

                if (objetivo == null)
                {
                    return Json(new { success = false, message = "Objetivo não encontrado." });
                }

                var novoValorAtual = objetivo.ValorAlvo + valor;

                var dto = new AtualizarObjetivoDto
                {
                    Id = objetivo.Id,
                    Descricao = objetivo.Descricao,
                    ValorAlvo = objetivo.ValorAlvo,
                    ValorTotal = novoValorAtual,
                    DataInicio = objetivo.DataInicio
                };

                await _objetivoService.AtualizarObjetivoAsync(dto);

                var percentual = objetivo.ValorAlvo > 0 ? (novoValorAtual / objetivo.ValorAlvo * 100) : 0;

                return Json(new
                {
                    success = true,
                    message = "Valor adicionado com sucesso!",
                    valorAtual = novoValorAtual,
                    percentual = Math.Round(percentual, 2)
                });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Erro ao adicionar valor: {ex.Message}" });
            }
        }
    }
}
