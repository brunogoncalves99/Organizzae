namespace Organizzae.Application.DTOs.Despesa
{
    public class DespesaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid CategoriaId { get; set; }
        public string CategoriaNome { get; set; } = string.Empty;
        public string? CategoriaIcone { get; set; }
        public string CategoriaCor { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public string ValorFormatado { get; set; } = string.Empty;
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? FormaPagamento { get; set; }
        public string TipoRecorrencia { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public string? AnexoComprovante { get; set; }
        public int DiasParaVencimento { get; set; }
        public bool EstaVencida { get; set; }
    }
}
