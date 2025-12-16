using Organizzae.Domain.Entities;
using System.Linq.Expressions;


namespace Organizzae.Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : EntidadeBase
    {
        Task<T?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<IEnumerable<T>> BuscarAsync(Expression<Func<T, bool>> predicate);
        Task<T?> BuscarUnicoAsync(Expression<Func<T, bool>> predicate);
        Task<bool> ExisteAsync(Expression<Func<T, bool>> predicate);
        Task<int> ContarAsync(Expression<Func<T, bool>>? predicate = null);

        Task AdicionarAsync(T entity);
        Task AdicionarVariosAsync(IEnumerable<T> entities);

        void Atualizar(T entity);
        void AtualizarVarios(IEnumerable<T> entities);

        void Remover(T entity);
        void RemoverVarios(IEnumerable<T> entities);

        Task<IEnumerable<T>> ObterPaginadoAsync(int pagina, int tamanhoPagina,
            Expression<Func<T, bool>>? filtro = null,
            Expression<Func<T, object>>? ordenarPor = null,
            bool ordenarDecrescente = false);
    }
}
