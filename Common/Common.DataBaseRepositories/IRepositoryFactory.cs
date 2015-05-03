// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

namespace Common.DatabaseRepositories
{
    /// <summary>
    /// Creates repositories.
    /// </summary>
    public interface IRepositoryFactory
    {
        /// <summary>
        /// Creates repository.
        /// </summary>
        /// <typeparam name="TEntity">Type of repository.</typeparam>
        IRepository<TEntity> CreateRepository<TEntity>() where TEntity : class;
    }
}