namespace Organizzae.Application.DTOs.Objetivo
{
    public class CriarObjetivoDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorInicial { get; set; }
        public DateTime DataAlvo { get; set; }
    }
}
