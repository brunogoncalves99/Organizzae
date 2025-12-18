namespace Organizzae.Application.DTOs.Usuario
{
    public class AlterarSenhaDto
    {
        public Guid UsuarioId { get; set; }
        public string SenhaAtual { get; set; } = string.Empty;
        public string NovaSenha { get; set; } = string.Empty;
        public string ConfirmacaoNovaSenha { get; set; } = string.Empty;
    }
}
