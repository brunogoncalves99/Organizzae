namespace Organizzae.Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FotoPerfil { get; set; }
        public DateTime? UltimoAcesso { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
