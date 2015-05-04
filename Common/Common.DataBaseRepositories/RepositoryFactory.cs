// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.DataContracts;

namespace Common.DatabaseRepositories
{
    /// <summary>
    /// Creates repositories.
    /// </summary>
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly IList<Type> _registratedEntities = new List<Type>()
            {
                typeof(User)
            };

        /// <summary>
        /// Creates repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of repository.</typeparam>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "Dispose it after usage.")]
        public IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class
        {
            var context = new TActiveContext();
            
            if (_registratedEntities.Contains(typeof(TEntity)))
            {
                return new Repository<TEntity, TActiveContext>(context);
            }

            throw new NotSupportedException(string.Format("DataBase doesn't contain table mapped from type {0}", typeof(TEntity)));
        }
    }
}