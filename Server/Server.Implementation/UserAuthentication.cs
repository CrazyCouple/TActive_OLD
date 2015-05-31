// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.IdentityModel.Selectors;
using System.Linq;
using System.ServiceModel;
using Common.DatabaseRepositories;
using Common.DataContracts;
using Common.Implementation.Hash;
using Common.Implementation.IocInWCF;
using Microsoft.Practices.Unity;

namespace Server.Implementation
{
    /// <summary>
    /// Authenticates users that call service operations.
    /// </summary>
    public class UserAuthentication : UserNamePasswordValidator
    {
        /// <summary>
        /// When overridden in a derived class, validates the specified username and password.
        /// </summary>
        /// <param name="userName">The username to validate.</param>
        /// <param name="password">The password to validate.</param>
        public override void Validate(string userName, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(userName) || userName.Equals("Anonymous"))
                {
                    // Allow anonymous access here.
                    return;
                }

                var container = UnityContainerHolder.UnityContainer;
                var repositoryFactory = container.Resolve<IRepositoryFactory>();
                var userRepository = repositoryFactory.CreateRepository<User>();

                var user = userRepository.FindBy(x => x.AccountName.Equals(userName, StringComparison.OrdinalIgnoreCase)).Single();

                if (user == null)
                {
                    throw new FaultException("Unknown Username or Incorrect Password");
                }

                var hashCalculator = container.Resolve<IHashCalculator>();

                if (!user.PasswordHash.Equals(hashCalculator.Compute(password).ToString()))
                {
                    throw new FaultException("Unknown Username or Incorrect Password");
                }
            }
            catch (Exception e)
            {
                // TODO: study wcf best practicis in exception handling. To read -> http://extremeexperts.com/Net/Articles/ExceptionHandlingInWCF.aspx
                throw new FaultException<Exception>(e, new FaultReason("Authentication failed."));
            }
        } 
    }
}