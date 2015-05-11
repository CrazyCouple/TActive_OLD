// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Authentication;
using System.Security.Principal;
using System.Threading;
using Common.DatabaseRepositories;
using Common.DataContracts;
using Common.Implementation.Hash;

namespace Server.Services
{
    /// <summary>
    /// Registers new user, saves its account name and password in the DB.
    /// </summary>
    public class RegistrationService : IRegistrationService
    {
        private const int MinPasswordLength = 6;
        private readonly IRepository<User> _userRepository;
        private readonly IHashCalculator _hashCalculator;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegistrationService"/> class.
        /// </summary>
        /// <param name="repositoryFactory">Creates repositories.</param>
        /// <param name="hashCalculator">The hash calculator.</param>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "We get it from IoC container.")]
        public RegistrationService(IRepositoryFactory repositoryFactory, IHashCalculator hashCalculator)
        {
            _userRepository = repositoryFactory.CreateRepository<User>();
            _hashCalculator = hashCalculator;
        }

        /// <summary>
        /// Registers new user, saves its account name and password in the DB.
        /// </summary>
        public void Register(string accountName, string password, bool isAdmin)
        {
            var currentPrincipal = Thread.CurrentPrincipal;
            ValidatePrincipal(currentPrincipal);

            var principalIsAdmin = currentPrincipal.IsInRole("Administrator");
            if (isAdmin && !principalIsAdmin)
            {
                throw new InvalidOperationException("Only administrator can register new administrator.");
            }

            ValidateCredentials(accountName, password);

            var user = CreateUser(accountName, password, isAdmin);
            _userRepository.Add(user);
        }

        private static void ValidatePrincipal(IPrincipal currentPrincipal)
        {
            if (currentPrincipal == null)
            {
                throw new ArgumentNullException("currentPrincipal");
            }

            if (!currentPrincipal.Identity.IsAuthenticated)
            {
                throw new AuthenticationException("Can not execute operation for unauthenticated identity.");
            }
        }

        private User CreateUser(string accountName, string password, bool isAdmin)
        {
            var passwordHash = _hashCalculator.ComputeHashFromString(password);
            var userProfile = new UserProfile { AccountName = accountName };
            var lastAddedUser = _userRepository.GetAll().OrderByDescending(x => x.Id).FirstOrDefault();
            var id = lastAddedUser == null ? 1 : lastAddedUser.Id + 1;

            var user = new User(userProfile, id)
                       {
                           PasswordHash = _hashCalculator.StringRepresentation(passwordHash),
                           IsAdmin = isAdmin
                       };

            return user;
        }

        private void ValidateCredentials(string accountName, string password)
        {
            if (string.IsNullOrEmpty(accountName))
            {
                throw new ArgumentNullException("accountName");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("password");
            }

            if (password.Length < MinPasswordLength)
            {
                throw new ArgumentException("Password length can not be less then 6 characters.");
            }

            var userWithSameAccountName = _userRepository.FindBy(x => x.Profile.AccountName.Equals(accountName));
            if (userWithSameAccountName != null)
            {
                throw new ArgumentException("User with same account name already exist.");
            }
        }
    }
}