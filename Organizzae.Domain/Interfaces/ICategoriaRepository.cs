using Organizzae.Domain.Entities;
using Organizzae.Domain.Enums;

namespace Organizzae.Domain.Interfaces
{
    public interface ICategoriaRepository : IRepositoryBase<Categoria>
    {
        Task<IEnumerable<Categoria>> ObterPorTipoAsync(TipoCategoria tipo);
        Task<IEnumerable<Categoria>> ObterCategoriasParaDespesaAsync();
        Task<IEnumerable<Categoria>> ObterCategoriasParaReceitaAsync();
        Task<IEnumerable<Categoria>> ObterCategoriasPadraoAsync();
        Task<IEnumerable<Categoria>> ObterCategoriasPersonalizadasAsync();
        Task<bool> NomeExisteAsync(string nome, TipoCategoria tipo);
        Task<Categoria?> ObterPorNomeAsync(string nome);
    }
}
