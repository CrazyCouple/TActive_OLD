// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace Common.DatabaseRepositories
{
    /// <summary>
    /// Provides access to entities in a storage.
    /// </summary>
    /// <typeparam name="TEntity">The type of elements that are presented in the storage.</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class 
    {
        /// <summary>
        /// Adds new entity to the storage.
        /// </summary>
        /// <param name="entity">The type of elements that are presented in the storage.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes entity from the storage.
        /// </summary>
        /// <param name="entity">The type of elements that are presented in the storage.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Gets all entities from the storage.
        /// </summary>
        /// <returns>The list of requested elements that are saved in the storage.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "The mathod is appropriate. Specific implementation executes complex algorithms.")]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all entities by predicate.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <returns>The list of requested elements that returns true for predicate.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "LINQ to entities need Expression, we can not wrapp func to expression - LINQ will throw exception during execution.")]
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}