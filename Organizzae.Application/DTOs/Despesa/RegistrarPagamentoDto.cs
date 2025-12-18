namespace Organizzae.Application.DTOs.Despesa
{
    public class RegistrarPagamentoDto
    {
        public Guid DespesaId { get; set; }
        public DateTime DataPagamento { get; set; }
        public string FormaPagamento { get; set; } = string.Empty;
    }
}
