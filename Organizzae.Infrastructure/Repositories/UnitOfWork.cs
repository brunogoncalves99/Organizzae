using Microsoft.EntityFrameworkCore.Storage;
using Organizzae.Domain.Interfaces;
using Organizzae.Infrastructure.Data.Context;

namespace Organizzae.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OrganizzaeDbContext _context;
        private IDbContextTransaction? _transaction;

        private IUsuarioRepository? _usuarios;
        private IDespesaRepository? _despesas;
        private IReceitaRepository? _receitas;
        private IObjetivoRepository? _objetivos;
        private ICategoriaRepository? _categorias;

        public UnitOfWork(OrganizzaeDbContext context)
        {
            _context = context;
        }

        public IUsuarioRepository Usuarios
        {
            get
            {
                _usuarios ??= new UsuarioRepository(_context);
                return _usuarios;
            }
        }
        public IDespesaRepository Despesas
        {
            get
            {
                _despesas ??= new DespesaRepository(_context);
                return _despesas;
            }
        }

        public IReceitaRepository Receitas
        {
            get
            {
                _receitas ??= new ReceitaRepository(_context);
                return _receitas;
            }
        }

        public IObjetivoRepository Objetivos
        {
            get
            {
                _objetivos ??= new ObjetivoRepository(_context);
                return _objetivos;
            }
        }

        public ICategoriaRepository Categorias
        {
            get
            {
                _categorias ??= new CategoriaRepository(_context);
                return _categorias;
            }
        }

        /// <summary>
        /// Salva todas as alterações pendentes no banco de dados
        /// </summary>
        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Inicia uma transação
        /// </summary>
        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Confirma a transação atual
        /// </summary>
        public async Task CommitTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Não há transação ativa para confirmar");

            try
            {
                await _context.SaveChangesAsync();
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackTransactionAsync();
                throw;
            }
            finally
            {
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <summary>
        /// Reverte a transação atual
        /// </summary>
        public async Task RollbackTransactionAsync()
        {
            if (_transaction == null)
                throw new InvalidOperationException("Não há transação ativa para reverter");

            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        /// <summary>
        /// Libera recursos
        /// </summary>
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
