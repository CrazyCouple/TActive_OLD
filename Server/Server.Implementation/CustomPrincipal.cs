// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using Common.DatabaseRepositories;
using Common.DataContracts;
using Common.Implementation.IocInWCF;
using Microsoft.Practices.Unity;

namespace Server.Implementation
{
    /// <summary>
    /// During user authorization user became principal.
    /// </summary>
    public class CustomPrincipal : IPrincipal
    {
        private readonly IIdentity _identity;

        private readonly List<string> _roles = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomPrincipal"/> class.
        /// </summary>
        /// <param name="identity">A custom identity</param>
        public CustomPrincipal(IIdentity identity)
        {
            _identity = identity;
        }

        /// <summary>
        /// Gets a custom identity.
        /// </summary>
        public IIdentity Identity
        {
            get { return _identity; }
        }

        /// <summary>
        /// Determines whether the current principal belongs to the specified role.
        /// </summary>
        /// <param name="role">The name of the role for which to check membership.</param>
        public bool IsInRole(string role)
        {
            var userRepository = UnityContainerHolder.UnityContainer.Resolve<IRepositoryFactory>().CreateRepository<User>();
            var userWithSameAccountName = userRepository.FindBy(x => string.Compare(x.AccountName, _identity.Name, StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();

            if (_identity.IsAuthenticated)
            {
                _roles.Add("User");
            }

            if (userWithSameAccountName != null && userWithSameAccountName.IsAdmin)
            {
                _roles.Add("Administrator");
            }

            return _roles.Contains(role);
        }
    }
}