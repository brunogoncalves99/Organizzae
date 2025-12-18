namespace Organizzae.Application.DTOs.Objetivo
{
    public class ObjetivoDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorAlvo { get; set; }
        public string ValorTotalFormatado { get; set; } = string.Empty;
        public decimal ValorEconomizado { get; set; }
        public string ValorEconomizadoFormatado { get; set; } = string.Empty;
        public decimal ValorRestante { get; set; }
        public string ValorRestanteFormatado { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public DateTime DataAlvo { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? ImagemRepresentativa { get; set; }
        public decimal PercentualProgresso { get; set; }
        public int DiasRestantes { get; set; }
        public decimal ValorMensalNecessario { get; set; }
        public string ValorMensalNecessarioFormatado { get; set; } = string.Empty;
        public bool EstaDentroDoPrazo { get; set; }
    }
}
