using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Organizzae.Application.DTOs.Despesa;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;
using System.Security.Claims;

namespace Organizzae.Web.Controllers
{
    [Authorize]
    public class DespesasController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DespesasController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var usuarioId = GetUsuarioId();
            var despesas = await _uow.Despesas.ObterPorUsuarioAsync(usuarioId);
            var despesasDto = _mapper.Map<List<DespesaDto>>(despesas);

            return View(despesasDto);
        }

        [HttpGet]
        public async Task<IActionResult> Criar()
        {
            await CarregarCategoriasViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(CriarDespesaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCategoriasViewBag();
                return View(dto);
            }

            try
            {
                dto.UsuarioId = GetUsuarioId();
                var despesa = _mapper.Map<Despesa>(dto);

                await _uow.Despesas.AdicionarAsync(despesa);
                await _uow.CommitAsync();

                TempData["SuccessMessage"] = "Despesa cadastrada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao cadastrar despesa: {ex.Message}");
                await CarregarCategoriasViewBag();
                return View(dto);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Editar(Guid id)
        {
            var despesa = await _uow.Despesas.ObterPorIdAsync(id);

            if (despesa == null || despesa.UsuarioId != GetUsuarioId())
                return NotFound();

            var dto = new AtualizarDespesaDto
            {
                Id = despesa.Id,
                CategoriaId = despesa.CategoriaId,
                Descricao = despesa.Descricao,
                Valor = despesa.Valor.Valor,
                DataVencimento = despesa.DataVencimento,
                TipoRecorrencia = despesa.TipoRecorrencia.ToString(),
                Observacoes = despesa.Observacoes
            };

            await CarregarCategoriasViewBag();
            return View(dto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(AtualizarDespesaDto dto)
        {
            if (!ModelState.IsValid)
            {
                await CarregarCategoriasViewBag();
                return View(dto);
            }

            try
            {
                var despesa = await _uow.Despesas.ObterPorIdAsync(dto.Id);

                if (despesa == null || despesa.UsuarioId != GetUsuarioId())
                    return NotFound();

                var valor = new Dinheiro(dto.Valor);
                var tipoRecorrencia = Enum.Parse<TipoRecorrencia>(dto.TipoRecorrencia);

                despesa.Atualizar(
                    dto.Descricao,
                    valor,
                    dto.DataVencimento,
                    dto.CategoriaId,
                    tipoRecorrencia
                );

                if (!string.IsNullOrWhiteSpace(dto.Observacoes))
                    despesa.DefinirObservacoes(dto.Observacoes);

                _uow.Despesas.Atualizar(despesa);
                await _uow.CommitAsync();

                TempData["SuccessMessage"] = "Despesa atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Erro ao atualizar despesa: {ex.Message}");
                await CarregarCategoriasViewBag();
                return View(dto);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarPagamento(RegistrarPagamentoDto dto)
        {
            try
            {
                var despesa = await _uow.Despesas.ObterPorIdAsync(dto.DespesaId);

                if (despesa == null || despesa.UsuarioId != GetUsuarioId())
                    return NotFound();

                var formaPagamento = Enum.Parse<FormaPagamento>(dto.FormaPagamento);
                despesa.RegistrarPagamento(dto.DataPagamento, formaPagamento);

                _uow.Despesas.Atualizar(despesa);
                await _uow.CommitAsync();

                TempData["SuccessMessage"] = "Pagamento registrado com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao registrar pagamento: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Excluir(Guid id)
        {
            try
            {
                var despesa = await _uow.Despesas.ObterPorIdAsync(id);

                if (despesa == null || despesa.UsuarioId != GetUsuarioId())
                    return NotFound();

                _uow.Despesas.Remover(despesa);
                await _uow.CommitAsync();

                TempData["SuccessMessage"] = "Despesa excluída com sucesso!";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao excluir despesa: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task CarregarCategoriasViewBag()
        {
            var categorias = await _uow.Categorias.ObterCategoriasParaDespesaAsync();
            ViewBag.Categorias = new SelectList(categorias, "Id", "Nome");
        }

        private Guid GetUsuarioId()
        {
            var usuarioIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.Parse(usuarioIdClaim ?? throw new UnauthorizedAccessException());
        }
    }
}
