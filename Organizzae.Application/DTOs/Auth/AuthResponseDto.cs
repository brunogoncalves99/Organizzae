namespace Organizzae.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? FotoPerfil { get; set; }
        public bool Sucesso { get; set; }
        public string? Mensagem { get; set; }
    }
}
