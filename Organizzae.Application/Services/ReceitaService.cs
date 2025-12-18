using AutoMapper;
using Organizzae.Application.DTOs.Receita;
using Organizzae.Application.Interfaces;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Interfaces;

namespace Organizzae.Application.Services;

public class ReceitaService : IReceitaService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ReceitaService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReceitaDto>> ObterTodasReceitasPorUsuarioAsync(Guid usuarioId)
    {
        var receitas = await _unitOfWork.Receitas.ObterPorUsuarioAsync(usuarioId);
        return _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
    }

    public async Task<ReceitaDto?> ObterReceitaPorIdAsync(Guid id)
    {
        var receita = await _unitOfWork.Receitas.ObterPorIdAsync(id);
        return receita == null ? null : _mapper.Map<ReceitaDto>(receita);
    }

    public async Task<ReceitaDto> CriarReceitaAsync(CriarReceitaDto dto)
    {
        var receita = _mapper.Map<Receita>(dto);
        receita.Id = Guid.NewGuid();
        receita.DataCriacao = DateTime.Now;

        await _unitOfWork.Receitas.AdicionarAsync(receita);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ReceitaDto>(receita);
    }

    public async Task AtualizarReceitaAsync(AtualizarReceitaDto dto)
    {
        var receita = await _unitOfWork.Receitas.ObterPorIdAsync(dto.Id);

        if (receita == null)
            throw new Exception("Receita não encontrada.");

        receita.Descricao = dto.Descricao;
        receita.Valor = dto.Valor;
        receita.Data = dto.Data;
        receita.Recorrente = dto.Recorrente;
        receita.DataAtualizacao = DateTime.Now;

        _unitOfWork.Receitas.Atualizar(receita);
        await _unitOfWork.CommitAsync();
    }

    public async Task ExcluirReceitaAsync(Guid id)
    {
        var receita = await _unitOfWork.Receitas.ObterPorIdAsync(id);

        if (receita == null)
            throw new Exception("Receita não encontrada.");

        _unitOfWork.Receitas.Remover(receita);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<ReceitaDto>> ObterReceitasPorMesAnoAsync(Guid usuarioId, int mes, int ano)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var receitas = await _unitOfWork.Receitas.ObterPorUsuarioEPeriodoAsync(usuarioId, dataInicio, dataFim);
        return _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
    }

    public async Task<decimal> ObterTotalReceitasPorMesAnoAsync(Guid usuarioId, int mes, int ano)
    {
        var dataInicio = new DateTime(ano, mes, 1);
        var dataFim = dataInicio.AddMonths(1).AddDays(-1);

        var total = await _unitOfWork.Receitas.CalcularTotalPorPeriodoAsync(usuarioId, dataInicio, dataFim);
        return total.Valor; 
    }

    public async Task<IEnumerable<ReceitaDto>> ObterReceitasRecorrentesAsync(Guid usuarioId)
    {
        var receitas = await _unitOfWork.Receitas.ObterRecorrentesAsync(usuarioId);
        return _mapper.Map<IEnumerable<ReceitaDto>>(receitas);
    }

    public async Task<decimal> ObterTotalReceitasRecorrentesAsync(Guid usuarioId)
    {
        var receitas = await _unitOfWork.Receitas.ObterRecorrentesAsync(usuarioId);
        return receitas.Sum(r => r.Valor);
    }
}
