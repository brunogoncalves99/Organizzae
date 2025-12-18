using AutoMapper;
using Organizzae.Application.DTOs.Dashboard;
using Organizzae.Application.Interfaces;
using Organizzae.Domain.Enums;
using Organizzae.Domain.Interfaces;

namespace Organizzae.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public DashboardService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ResumoFinanceiroDto> ObterResumoFinanceiroAsync(Guid usuarioId, DateTime dataInicio, DateTime dataFim)
        {
            // Calcular totais
            var totalReceitas = await _uow.Receitas.CalcularTotalPorPeriodoAsync(usuarioId, dataInicio, dataFim);
            var totalDespesas = await _uow.Despesas.CalcularTotalPorPeriodoAsync(usuarioId, dataInicio, dataFim);
            var saldo = totalReceitas - totalDespesas;

            // Calcular período anterior para comparação
            var diasPeriodo = (dataFim - dataInicio).Days;
            var dataInicioAnterior = dataInicio.AddDays(-diasPeriodo);
            var dataFimAnterior = dataInicio.AddDays(-1);

            var totalReceitasAnterior = await _uow.Receitas.CalcularTotalPorPeriodoAsync(usuarioId, dataInicioAnterior, dataFimAnterior);
            var totalDespesasAnterior = await _uow.Despesas.CalcularTotalPorPeriodoAsync(usuarioId, dataInicioAnterior, dataFimAnterior);

            // Calcular percentuais de variação
            var percentualVariacaoReceitas = CalcularPercentualVariacao(totalReceitasAnterior.Valor, totalReceitas.Valor);
            var percentualVariacaoDespesas = CalcularPercentualVariacao(totalDespesasAnterior.Valor, totalDespesas.Valor);

            // Contar despesas pendentes e atrasadas
            var despesasPendentes = await _uow.Despesas.ObterPorStatusAsync(usuarioId, StatusDespesa.Pendente);
            var despesasAtrasadas = await _uow.Despesas.ObterAtrasadasAsync(usuarioId);
            var receitasPendentes = await _uow.Receitas.ObterPorStatusAsync(usuarioId, StatusReceita.Pendente);

            // Obter despesas por categoria
            var despesasPorCategoria = await _uow.Despesas.ObterDespesasPorCategoriaAsync(usuarioId, dataInicio, dataFim);
            var receitasPorCategoria = await _uow.Receitas.ObterReceitasPorCategoriaAsync(usuarioId, dataInicio, dataFim);

            // Obter maiores gastos
            var maioresGastos = await _uow.Despesas.ObterMaioresGastosAsync(usuarioId, dataInicio, dataFim, 5);
            var maioresGastosDto = _mapper.Map<List<DespesaResumoDto>>(maioresGastos);

            // Obter próximos vencimentos
            var proximosVencimentos = await _uow.Despesas.ObterVencimentoProximoAsync(usuarioId, 7);
            var proximosVencimentosDto = _mapper.Map<List<DespesaResumoDto>>(proximosVencimentos);

            return new ResumoFinanceiroDto
            {
                TotalReceitas = totalReceitas.Valor,
                TotalReceitasFormatado = totalReceitas.ObterFormatado(),
                TotalDespesas = totalDespesas.Valor,
                TotalDespesasFormatado = totalDespesas.ObterFormatado(),
                Saldo = saldo.Valor,
                SaldoFormatado = saldo.ObterFormatado(),
                PercentualVariacaoReceitas = percentualVariacaoReceitas,
                PercentualVariacaoDespesas = percentualVariacaoDespesas,
                DespesasPendentes = despesasPendentes.Count(),
                DespesasAtrasadas = despesasAtrasadas.Count(),
                ReceitasPendentes = receitasPendentes.Count(),
                DespesasPorCategoria = despesasPorCategoria,
                ReceitasPorCategoria = receitasPorCategoria,
                MaioresGastos = maioresGastosDto,
                ProximosVencimentos = proximosVencimentosDto,
                PeriodoInicio = dataInicio,
                PeriodoFim = dataFim
            };
        }

        public async Task<ResumoFinanceiroDto> ObterResumoMesAtualAsync(Guid usuarioId)
        {
            var dataInicio = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var dataFim = dataInicio.AddMonths(1).AddDays(-1);

            return await ObterResumoFinanceiroAsync(usuarioId, dataInicio, dataFim);
        }

        private decimal CalcularPercentualVariacao(decimal valorAnterior, decimal valorAtual)
        {
            if (valorAnterior == 0)
                return valorAtual > 0 ? 100 : 0;

            return Math.Round(((valorAtual - valorAnterior) / valorAnterior) * 100, 2);
        }
    }
}
