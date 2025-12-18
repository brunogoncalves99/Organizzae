namespace Organizzae.Application.DTOs.Categoria
{
    public class CategoriaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public string? Icone { get; set; }
        public string CorHexadecimal { get; set; } = string.Empty;
        public bool EhPadrao { get; set; }
    }
}
