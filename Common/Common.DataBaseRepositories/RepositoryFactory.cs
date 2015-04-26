// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Common.DataContracts;

namespace DataBaseRepositories
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
        /// Creates repositories.
        /// </summary>
        /// <typeparam name="TEntity">Type of repository.</typeparam>
        [SuppressMessage("Microsoft.Reliability", "CA2000:Ликвидировать объекты перед потерей области", Justification = "We don't need it.")]
        public IRepository<TEntity> CteateRepository<TEntity>() where TEntity : class
        {
            var context = new TActiveContext();

            if (_registratedEntities.Contains(typeof(User)))
            {
                return new Repository<TEntity, TActiveContext>(context);
            }

            throw new NotSupportedException(string.Format("In DataBase it doesn't exist table mapped from type {0}", typeof(TEntity)));
        }
    }
}