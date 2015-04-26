// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
using System.Data.Entity;
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
    /// <typeparam name="T">EF DbContext.</typeparam>
    public abstract class RepositoryBase<T> : IDisposable where T : DbContext, new()
    {
        private T _context;

        /// <summary>
        /// Gets or sets the class logger.
        /// </summary>
        /// <value>Logger.</value>
        protected abstract Logger Logger { get; set; }

        /// <summary>
        /// Gets or creates DBContext.
        /// </summary>
        /// <value>EF DbContext.</value>
        protected virtual T DataContext
        {
            get { return _context ?? (_context = new T()); }
        }

        /// <summary>
        /// Returns searched entity.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <returns> The searched elements from DB.</returns>
        [SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "Get", Justification = "I like it!")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No")]
        protected virtual TEntity Get<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (predicate == null) throw new ArgumentNullException("predicate");

            return DataContext.Set<TEntity>().Where(predicate).SingleOrDefault();
        }

        /// <summary>
        /// Returns searched entities.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "OK")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No")]
        protected virtual IQueryable<TEntity> GetList<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            if (predicate == null) throw new ArgumentNullException("predicate");

            try
            {
                return DataContext.Set<TEntity>().Where(predicate);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Returns searched entities in requested order.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <param name="orderBy">Predicate for ordering.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <typeparam name="TKey">Ordering key.</typeparam>
        /// <returns></returns>Searched type.
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "No")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No")]
        protected virtual IQueryable<TEntity> GetList<TEntity, TKey>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TKey>> orderBy) where TEntity : class
        {
            try
            {
                return GetList(predicate).OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
                Logger.ErrorException(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Returns all entities in requested order.
        /// </summary>
        /// <param name="orderBy">Predicate for ordering.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <typeparam name="TKey">Ordering key.</typeparam>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "No")]
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "No")]
        protected virtual IQueryable<TEntity> GetList<TEntity, TKey>(Expression<Func<TEntity, TKey>> orderBy) where TEntity : class
        {
            try
            {
                return GetList<TEntity>().OrderBy(orderBy);
            }
            catch (Exception ex)
            {
                //Log error
                Logger.ErrorException(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// Returns all entities.
        /// </summary>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <returns></returns>
        [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "No")]
        protected virtual IQueryable<TEntity> GetList<TEntity>() where TEntity : class
        {
            try
            {
                return DataContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                //Log error
                Logger.ErrorException(ex.Message, ex);
            }
            return null;
        }

        /// <summary>
        /// The requested changes are migrated to real DB.
        /// </summary>
        /// <param name="entity">Searched type.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <returns></returns>
        protected virtual bool Save<TEntity>(TEntity entity) where TEntity : class
        {

            try
            {
                return DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Error saving " + typeof(TEntity) + ".", ex);
                throw;
            }

        }

        /// <summary>
        /// Adds new entity to DB and saves changes.
        /// </summary>
        /// <param name="entity">Searched type.</param>
        /// <typeparam name="TEntity">Searched type.</typeparam>
        /// <returns></returns>
        protected virtual bool Update<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                DataContext.Set<TEntity>().Attach(entity);
                return DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Error saving " + typeof(TEntity) + ".", ex);
                throw;
            }
        }

        /// <summary>
        /// Deletes entity from DB.
        /// </summary>
        /// <param name="entity">Searched type</param>
        /// <typeparam name="TEntity">Searched type</typeparam>
        /// <returns></returns>
        protected virtual bool Delete<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                var objectSet = ((IObjectContextAdapter)DataContext).ObjectContext.CreateObjectSet<TEntity>();
                objectSet.Attach(entity);
                objectSet.DeleteObject(entity);
                return DataContext.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Logger.ErrorException("Error deleting " + typeof(TEntity), ex);
                throw;
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

        private void Dispose(bool item)
        {
            if (item)
            {
                if (DataContext != null) DataContext.Dispose();
            }
        }
    }
}