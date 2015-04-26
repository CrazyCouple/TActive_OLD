// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace DataBaseRepositories
{
    /// <summary>
    /// Provides access to entities in DB.
    /// </summary>
    /// <typeparam name="TEntity">The type of elements that are/have to be presented in DB.</typeparam>
    public interface IRepository<TEntity> where TEntity : class 
    {
        /// <summary>
        /// Adds new entity to DB.
        /// </summary>
        /// <param name="entity">The type of elements that are/have to be presented in DB.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes entity from DB.
        /// </summary>
        /// <param name="entity">The type of elements that are/have to be presented in DB.</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Gets all entities with specified type from DB.
        /// </summary>
        /// <returns>The list of requested elements that are saved in DB.</returns>
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "I like it.")]
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Gets all entities that returns true for predicate.
        /// </summary>
        /// <param name="predicate">Predicate for searching.</param>
        /// <returns>The list of requested elements that returns true for predicate.</returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures", Justification = "I don't see any problem.")]
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
    }
}