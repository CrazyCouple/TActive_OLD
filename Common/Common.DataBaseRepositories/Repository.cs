// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using NLog;

namespace Common.DatabaseRepositories
{
    /// <summary>
    /// Provides base methods for working with entities in a DB.
    /// </summary>
    /// <typeparam name="TEntity">The type of elements manipulated by the TContext.</typeparam>
    /// <typeparam name="TContext">The type which provides functionality to create, delete and manipulate real DB.</typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly TContext _context;
        private readonly Logger _logger;
        private readonly string _name;

        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity,TContext}" /> class.
        /// </summary>
        /// <param name="context">The DBContext.</param>
        public Repository(TContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
            _name = GetType() + "[" + typeof(TEntity) + ", " + typeof(TContext) + "]";
            _logger = LogManager.GetLogger(_name);
            _disposed = false;
        }

        /// <summary>
        /// Adds new user to the DB. Commits changes.
        /// </summary>
        public void Add(TEntity entity)
        {
            ThrowIfDisposed();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            ExecuteAndHandleDbExceptions(() =>
                               {
                                   _context.Set<TEntity>().Add(entity);
                                   Save();

                                   _logger.Info("Entity ({0}) added. Changes are committed.", entity.ToString());
                               });
        }

        /// <summary>
        /// Deletes user from DB. Commits changes.
        /// </summary>
        public void Delete(TEntity entity)
        {
            ThrowIfDisposed();

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            ExecuteAndHandleDbExceptions(() =>
                               {
                                   var objectSet = ((IObjectContextAdapter)_context).ObjectContext.CreateObjectSet<TEntity>();
                                   objectSet.Attach(entity);
                                   objectSet.DeleteObject(entity);
                                   Save();

                                   _logger.Info("Entity ({0}) deleted. Changes are committed.", entity.ToString());
                               });
        }

        /// <summary>
        /// Gets all entities from the DB.
        /// </summary>
        public IEnumerable<TEntity> GetAll()
        {
            ThrowIfDisposed();

            return ExecuteAndHandleDbExceptions(() => _context.Set<TEntity>());
        }

        /// <summary>
        /// Gets all entities by predicate.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        public IEnumerable<TEntity> FindBy(Func<TEntity, bool> predicate)
        {
            ThrowIfDisposed();

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            Expression<Func<TEntity, bool>> exp = (x) => predicate(x); // TODO: Invesgtigate Expression and Func performance - Database or RAM.

            // TODO: Write a unit test to see what works faster:  GetAll().Where(predicate) or string below.
            return ExecuteAndHandleDbExceptions(() => _context.Set<TEntity>().Where(exp));
        }

        /// <summary>
        /// Disposes the <see cref="Repository{TEntity,TContext}"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            _disposed = true;

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes DBContext.
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        /// <summary>
        /// The requested changes are migrated to real DB.
        /// </summary>
        private void Save()
        {
            _context.SaveChanges();
        }

        private void ExecuteAndHandleDbExceptions(Action action)
        {
            ExecuteAndHandleDbExceptions(() =>
                               {
                                   action();
                                   return null;
                               });
        }

        private IEnumerable<TEntity> ExecuteAndHandleDbExceptions(Func<IEnumerable<TEntity>> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                const string Message = "Problem with EntityFramework command execution.";
                _logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(_name);
            }
        }
    }
}