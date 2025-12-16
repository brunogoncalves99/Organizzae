namespace Organizzae.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IDespesaRepository Despesas { get; }
        IReceitaRepository Receitas { get; }
        IObjetivoRepository Objetivos { get; }
        ICategoriaRepository Categorias { get; }

        /// <summary>
        /// Salva todas as alterações pendentes no banco de dados
        /// </summary>
        Task<int> CommitAsync();

        /// <summary>
        /// Inicia uma transação
        /// </summary>
        Task BeginTransactionAsync();

        /// <summary>
        /// Confirma a transação atual
        /// </summary>
        Task CommitTransactionAsync();

        /// <summary>
        /// Reverte a transação atual
        /// </summary>
        Task RollbackTransactionAsync();
    }
}
