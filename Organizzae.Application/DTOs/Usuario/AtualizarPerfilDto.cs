namespace Organizzae.Application.DTOs.Usuario
{
    public class AtualizarPerfilDto
    {
        public Guid UsuarioId { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
