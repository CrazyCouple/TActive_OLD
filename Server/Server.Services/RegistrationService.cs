// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Linq;
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
        public RegistrationService(IRepositoryFactory repositoryFactory, IHashCalculator hashCalculator)
        {
            if (repositoryFactory == null)
            {
                throw new ArgumentNullException("repositoryFactory");
            }

            if (hashCalculator == null)
            {
                throw new ArgumentNullException("hashCalculator");
            }

            _userRepository = repositoryFactory.CreateRepository<User>();
            _hashCalculator = hashCalculator;
        }

        /// <summary>
        /// Registers new user, saves its account name and password in the DB.
        /// </summary>
        public void Register(string accountName, string password, bool isAdmin)
        {
            var principalIsAdmin = Thread.CurrentPrincipal.IsInRole("Administrator");

            if (isAdmin && !principalIsAdmin)
            {
                throw new InvalidOperationException("Only administrator can register new administrator.");
            }

            ValidateCredentials(accountName, password);

            var user = CreateUser(accountName, password, isAdmin);
            _userRepository.Add(user);
        }
     
        private User CreateUser(string accountName, string password, bool isAdmin)
        {
            return new User(accountName)
                   {
                       PasswordHash = _hashCalculator.Compute(password).ToString(),
                       IsAdmin = isAdmin
                   };
        }

        private void ValidateCredentials(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentNullException("accountName");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentNullException("password");
            }

            if (password.Length < MinPasswordLength)
            {
                throw new ArgumentException(string.Format("Password length can not be less then {0} characters.", MinPasswordLength));
            }

            var userWithSameAccountName = _userRepository.FindBy(x => x.AccountName.Equals(accountName, StringComparison.OrdinalIgnoreCase));
            if (userWithSameAccountName.Any())
            {
                throw new ArgumentException(string.Format("User with account name = {0} already exist.", accountName));
            }
        }
    }
}