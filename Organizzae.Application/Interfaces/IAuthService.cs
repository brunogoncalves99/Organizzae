using Organizzae.Application.DTOs.Auth;

namespace Organizzae.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> RegistrarAsync(RegistroUsuarioDto dto);
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        string HashSenha(string senha);
        bool VerificarSenha(string senha, string senhaHash);
    }

}
