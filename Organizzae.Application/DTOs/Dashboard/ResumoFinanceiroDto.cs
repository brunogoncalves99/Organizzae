namespace Organizzae.Application.DTOs.Dashboard;

/// <summary>
/// DTO com resumo financeiro para o dashboard
/// </summary>
public class ResumoFinanceiroDto
{
    public decimal TotalReceitas { get; set; }
    public string TotalReceitasFormatado { get; set; } = string.Empty;

    public decimal TotalDespesas { get; set; }
    public string TotalDespesasFormatado { get; set; } = string.Empty;

    public decimal Saldo { get; set; }
    public string SaldoFormatado { get; set; } = string.Empty;

    public decimal PercentualVariacaoReceitas { get; set; }
    public decimal PercentualVariacaoDespesas { get; set; }

    public int DespesasPendentes { get; set; }
    public int DespesasAtrasadas { get; set; }
    public int ReceitasPendentes { get; set; }

    public Dictionary<string, decimal> DespesasPorCategoria { get; set; } = new();
    public Dictionary<string, decimal> ReceitasPorCategoria { get; set; } = new();

    public List<DespesaResumoDto> MaioresGastos { get; set; } = new();
    public List<DespesaResumoDto> ProximosVencimentos { get; set; } = new();

    public DateTime PeriodoInicio { get; set; }
    public DateTime PeriodoFim { get; set; }
}

/// <summary>
/// DTO resumido de despesa para o dashboard
/// </summary>
public class DespesaResumoDto
{
    public Guid Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public decimal Valor { get; set; }
    public string ValorFormatado { get; set; } = string.Empty;
    public DateTime DataVencimento { get; set; }
    public string CategoriaNome { get; set; } = string.Empty;
    public string CategoriaCor { get; set; } = string.Empty;
}
