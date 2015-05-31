// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.ServiceModel;

namespace Server.Services
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
        /// <param name="accountName">An account name of the user.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="isAdmin">Indicates whether user is an administrator or not.</param>
        [OperationContract]
        void Register(string accountName, string password, bool isAdmin);
    }
}