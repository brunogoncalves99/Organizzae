using Organizzae.Application.DTOs.Auth;
using Organizzae.Application.Interfaces;
using Organizzae.Domain.Entities;
using Organizzae.Domain.Interfaces;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _uow;

        public AuthService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<AuthResponseDto> RegistrarAsync(RegistroUsuarioDto dto)
        {
            try
            {
                var cpf = new CPF(dto.CPF);
                var email = new Email(dto.Email);

                // Verificar se CPF já existe
                if (await _uow.Usuarios.CpfExisteAsync(cpf))
                {
                    return new AuthResponseDto
                    {
                        Sucesso = false,
                        Mensagem = "CPF já cadastrado no sistema"
                    };
                }

                // Verificar se Email já existe
                if (await _uow.Usuarios.EmailExisteAsync(email))
                {
                    return new AuthResponseDto
                    {
                        Sucesso = false,
                        Mensagem = "E-mail já cadastrado no sistema"
                    };
                }

                // Criar hash da senha
                var senhaHash = HashSenha(dto.Senha);

                // Criar usuário
                var usuario = new Usuario(dto.Nome, cpf, email, senhaHash);

                await _uow.Usuarios.AdicionarAsync(usuario);
                await _uow.CommitAsync();

                return new AuthResponseDto
                {
                    UsuarioId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email.Endereco,
                    Sucesso = true,
                    Mensagem = "Usuário registrado com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Sucesso = false,
                    Mensagem = $"Erro ao registrar usuário: {ex.Message}"
                };
            }
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            try
            {
                var cpf = new CPF(dto.CPF);
                var usuario = await _uow.Usuarios.ObterPorCpfAsync(cpf);

                if (usuario == null)
                {
                    return new AuthResponseDto
                    {
                        Sucesso = false,
                        Mensagem = "CPF ou senha incorretos"
                    };
                }

                if (!VerificarSenha(dto.Senha, usuario.SenhaHash))
                {
                    return new AuthResponseDto
                    {
                        Sucesso = false,
                        Mensagem = "CPF ou senha incorretos"
                    };
                }

                // Registrar acesso
                usuario.RegistrarAcesso();
                await _uow.CommitAsync();

                return new AuthResponseDto
                {
                    UsuarioId = usuario.Id,
                    Nome = usuario.Nome,
                    Email = usuario.Email.Endereco,
                    FotoPerfil = usuario.FotoPerfil,
                    Sucesso = true,
                    Mensagem = "Login realizado com sucesso"
                };
            }
            catch (Exception ex)
            {
                return new AuthResponseDto
                {
                    Sucesso = false,
                    Mensagem = $"Erro ao realizar login: {ex.Message}"
                };
            }
        }

        public string HashSenha(string senha)
        {
            return BCrypt.Net.BCrypt.HashPassword(senha);
        }

        public bool VerificarSenha(string senha, string senhaHash)
        {
            return BCrypt.Net.BCrypt.Verify(senha, senhaHash);
        }
    }
}
