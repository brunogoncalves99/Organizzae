using AutoMapper;
using Organizzae.Application.DTOs.Objetivo;
using Organizzae.Application.Interfaces;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Interfaces;

namespace Organizzae.Application.Services;

public class ObjetivoService : IObjetivoService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ObjetivoService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ObjetivoDto>> ObterTodosObjetivosPorUsuarioAsync(Guid usuarioId)
    {
        var objetivos = await _unitOfWork.Objetivos.ObterPorUsuarioAsync(usuarioId);
        return _mapper.Map<IEnumerable<ObjetivoDto>>(objetivos);
    }

    public async Task<ObjetivoDto?> ObterObjetivoPorIdAsync(Guid id)
    {
        var objetivo = await _unitOfWork.Objetivos.ObterPorIdAsync(id);
        return objetivo == null ? null : _mapper.Map<ObjetivoDto>(objetivo);
    }

    public async Task<ObjetivoDto> CriarObjetivoAsync(CriarObjetivoDto dto)
    {
        var objetivo = _mapper.Map<Objetivo>(dto);

        await _unitOfWork.Objetivos.AdicionarAsync(objetivo);
        await _unitOfWork.CommitAsync();

        return _mapper.Map<ObjetivoDto>(objetivo);
    }

    public async Task AtualizarObjetivoAsync(AtualizarObjetivoDto dto)
    {
        var objetivo = await _unitOfWork.Objetivos.ObterPorIdAsync(dto.Id);

        if (objetivo == null)
            throw new Exception("Objetivo não encontrado.");

        objetivo.Descricao = dto.Descricao;
        objetivo.ValorTotal =  dto.ValorTotal;
        objetivo.DataInicio = dto.DataInicio;
        objetivo.DataAlvo = dto.DataAlvo;
        objetivo.DataConclusao = dto.DataFim;

        _unitOfWork.Objetivos.Atualizar(objetivo);
        await _unitOfWork.CommitAsync();
    }

    public async Task ExcluirObjetivoAsync(Guid id)
    {
        var objetivo = await _unitOfWork.Objetivos.ObterPorIdAsync(id);

        if (objetivo == null)
            throw new Exception("Objetivo não encontrado.");

        _unitOfWork.Objetivos.Remover(objetivo);
        await _unitOfWork.CommitAsync();
    }

    public async Task<IEnumerable<ObjetivoDto>> ObterObjetivosAtivosAsync(Guid usuarioId)
    {
        // Obter todos e filtrar em memória
        var objetivos = await _unitOfWork.Objetivos.ObterPorUsuarioAsync(usuarioId);
        var objetivosAtivos = objetivos.Where(o => !o.Concluido);
        return _mapper.Map<IEnumerable<ObjetivoDto>>(objetivosAtivos);
    }

    public async Task<IEnumerable<ObjetivoDto>> ObterObjetivosConcluidosAsync(Guid usuarioId)
    {
        // Obter todos e filtrar em memória
        var objetivos = await _unitOfWork.Objetivos.ObterPorUsuarioAsync(usuarioId);
        var objetivosConcluidos = objetivos.Where(o => o.Status);
        return _mapper.Map<IEnumerable<ObjetivoDto>>(objetivosConcluidos);
    }

    public async Task<decimal> ObterTotalValorAlvoAsync(Guid usuarioId)
    {
        var objetivos = await _unitOfWork.Objetivos.ObterPorUsuarioAsync(usuarioId);
        return objetivos.Sum(o => o.ValorAlvo.Valor); 
    }

    public async Task<decimal> ObterTotalValorAtualAsync(Guid usuarioId)
    {
        var objetivos = await _unitOfWork.Objetivos.ObterPorUsuarioAsync(usuarioId);
        return objetivos.Sum(o => o.ValorAtual.Valor); 
    }
}

