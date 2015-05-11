// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.ServiceModel;

namespace Common.DataContracts
{
    /// <summary>
    /// Service that registers new user in the system.
    /// </summary>
    [ServiceContract]
    public interface IRegistrationService
    {
        /// <summary>
        /// Registers new user in the system.
        /// </summary>
        /// <param name="accountName">An account name of user.</param>
        /// <param name="password">The users password.</param>
        /// <param name="isAdmin"> Indicates whether have to be user or not an administrator.</param>
        [OperationContract]
        void Register(string accountName, string password, bool isAdmin);
    }
}