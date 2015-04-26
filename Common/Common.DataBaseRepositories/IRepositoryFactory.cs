// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using Common.DataContracts;

namespace DataBaseRepositories
{
    /// <summary>
    /// Creates repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates repositories.
        /// </summary>
        /// <typeparam name="TEntity">Type of repository.</typeparam>
        IRepository<TEntity> CteateRepository<TEntity>() where TEntity : class;
    }
}