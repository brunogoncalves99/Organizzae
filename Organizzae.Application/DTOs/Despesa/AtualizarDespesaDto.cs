namespace Organizzae.Application.DTOs.Despesa
{
    public class AtualizarDespesaDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public string TipoRecorrencia { get; set; } = "Nenhuma";
        public string? Observacoes { get; set; }
    }
}
