namespace Organizzae.Application.DTOs.Receita
{
    public class ReceitaDto
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
        public DateTime DataPrevista { get; set; }
        public DateTime? DataRecebimento { get; set; }
        public string Status { get; set; } = string.Empty;
        public string TipoRecorrencia { get; set; } = string.Empty;
        public string? Observacoes { get; set; }
        public int DiasParaRecebimento { get; set; }
        public bool EstaAtrasada { get; set; }
    }
}
