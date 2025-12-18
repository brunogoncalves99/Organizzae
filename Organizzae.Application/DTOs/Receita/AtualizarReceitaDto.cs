namespace Organizzae.Application.DTOs.Receita
{
    public class AtualizarReceitaDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaId { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public decimal Valor { get; set; }
        public DateTime DataPrevista { get; set; }
        public string TipoRecorrencia { get; set; } = "Nenhuma";
        public string? Observacoes { get; set; }
    }
}
