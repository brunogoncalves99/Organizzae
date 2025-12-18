namespace Organizzae.Application.DTOs.Objetivo
{
    public class AtualizarObjetivoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataAlvo { get; set; }
    }
}
