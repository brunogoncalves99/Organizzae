using Organizzae.Domain.Entities;
using Organizzae.Domain.ValueObjects;

namespace Organizzae.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepositoryBase<Usuario>
    {
        Task<Usuario?> ObterPorCpfAsync(CPF cpf);
        Task<Usuario?> ObterPorEmailAsync(Email email);
        Task<bool> CpfExisteAsync(CPF cpf);
        Task<bool> EmailExisteAsync(Email email);
        Task<Usuario?> ObterComDespesasAsync(Guid usuarioId);
        Task<Usuario?> ObterComReceitasAsync(Guid usuarioId);
        Task<Usuario?> ObterComObjetivosAsync(Guid usuarioId);
        Task<Usuario?> ObterCompletoAsync(Guid usuarioId);
    }
}
