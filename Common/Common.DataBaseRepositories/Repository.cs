// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Infrastructure;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using NLog;

namespace DataBaseRepositories
{
    /// <summary>
    /// Provides base methods for working with entities.
    /// </summary>
    /// <typeparam name="TEntity">The mapped type.</typeparam>
    /// <typeparam name="TContext">Type of the used DbContext.</typeparam>
    public class Repository<TEntity, TContext> : IRepository<TEntity>, IDisposable
        where TEntity : class
        where TContext : DbContext, new()
    {
        private readonly TContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="Repository{TEntity,TContext}" /> class.
        /// </summary>
        /// <param name="context">The DBContext.</param>
        public Repository(TContext context)
        {
            _context = context;
            Logger = LogManager.GetLogger(GetType().ToString());
        }

        /// <summary>
        /// Gets or sets the class logger.
        /// </summary>
        /// <value>Logger.</value>
        private Logger Logger { get; set; }

        /// <summary>
        /// Gets or creates DBContext.
        /// </summary>
        /// <value>EF DbContext.</value>
        private TContext DataContext
        {
            get { return _context; }
        }

        /// <summary>
        /// Adds new user to DB.
        /// </summary>
        /// <param name="entity">The object to add.</param>
        public void Add(TEntity entity)
        {
            DataContext.Set(entity.GetType()).Add(entity);
            Save();
        }

        /// <summary>
        /// Deletes user from DB.
        /// </summary>
        /// <param name="entity">The object to delete.</param>
        public void Delete(TEntity entity)
        {
            try
            {
                var objectSet = ((IObjectContextAdapter)DataContext).ObjectContext.CreateObjectSet<TEntity>();
                objectSet.Attach(entity);
                objectSet.DeleteObject(entity);
            }
            catch (EntityCommandExecutionException ex)
            {
                const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (EntitySqlException ex)
            {
                const string Message = "Parsing Entity SQL command text error.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (Exception ex)
            {
                const string Message = "Some errore appears during EntityFramework command execution.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
        }

        /// <summary>
        /// Gets all entities with specified type from DB.
        /// </summary>
        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return DataContext.Set<TEntity>();
            }
            catch (EntityCommandExecutionException ex)
            {
                const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (EntitySqlException ex)
            {
                const string Message = "Parsing Entity SQL command text error.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (Exception ex)
            {
                const string Message = "Some errore appears during EntityFramework command execution.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
        }

        /// <summary>
        /// Gets all entities that returns true for predicate.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <returns>The list of requested elements that returns true for predicate.</returns>
        public IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            try
            {
                return DataContext.Set<TEntity>().Where(predicate);
            }
            catch (EntityCommandExecutionException ex)
            {
                const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (EntitySqlException ex)
            {
                const string Message = "Parsing Entity SQL command text error.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (Exception ex)
            {
                const string Message = "Some errore appears during EntityFramework command execution.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
        }

        /// <summary>
        /// Disposes DBContext.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        /// <summary>
        /// The requested changes are migrated to real DB.
        /// </summary>
        private void Save()
        {
            try
            {
                DataContext.SaveChanges();
            }
            catch (EntityCommandExecutionException ex)
            {
                const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (EntitySqlException ex)
            {
                const string Message = "Parsing Entity SQL command text error.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
            catch (Exception ex)
            {
                const string Message = "Some errore appears during EntityFramework command execution.";
                Logger.ErrorException(Message, ex);
                throw new RepositoryException(Message, ex);
            }
        }

        private void Dispose(bool item)
        {
            if (item)
            {
                if (DataContext != null)
                {
                    DataContext.Dispose();
                }
            }
        }

        ////TODO: Don't remove it, maybe methods will be useful.
        /////// <summary>
        /////// Adds new entity to DB and saves changes.
        /////// </summary>
        /////// <param name="entity">Searched object.</param>
        ////private bool Update(TEntity entity)
        ////{
        ////    try
        ////    {
        ////        DataContext.Set<TEntity>().Attach(entity);
        ////        return DataContext.SaveChanges() > 0;
        ////    }
        ////    catch (EntityCommandExecutionException ex)
        ////    {
        ////        const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////    catch (EntitySqlException ex)
        ////    {
        ////        const string Message = "Parsing Entity SQL command text error.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        const string Message = "Some errore appears during EntityFramework command execution.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////}
        
        /////// <summary>
        /////// Returns searched entity.
        /////// </summary>
        /////// <param name="predicate">Predicate for searching.</param>
        /////// <returns> The searched elements from DB.</returns>
        ////[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "I like it!")]
        ////[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No")]
        ////private TEntity Get(Expression<Func<TEntity, bool>> predicate)
        ////{
        ////    if (predicate == null)
        ////    {
        ////        throw new ArgumentNullException("predicate");
        ////    }

        ////    try
        ////    {
        ////        return DataContext.Set<TEntity>().Where(predicate).SingleOrDefault();
        ////    }
        ////    catch (EntityCommandExecutionException ex)
        ////    {
        ////        const string Message = "The underlying storage provider could not execute the specified command. Problem with EntityFramework.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////    catch (EntitySqlException ex)
        ////    {
        ////        const string Message = "Parsing Entity SQL command text error.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////        const string Message = "Some errore appears during EntityFramework command execution.";
        ////        Logger.ErrorException(Message, ex);
        ////        throw new RepositoryException(Message, ex);
        ////    }
        ////}
    }
}