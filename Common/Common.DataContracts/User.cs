// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DataContracts
{
    /// <summary>
    /// Represents a simple chat user.
    /// </summary>
    [DataContract]
    public class User
    {
        private readonly FriendsCollection _friends;
        private long _id;
        private string _accountName;
        private string _passwordHash;
        private UserProfile _profile;
        private bool _isAdmin;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// This constructor we need for DB working.
        /// </summary>
        public User()
        {
            _friends = new FriendsCollection();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="accountName">The account name.</param>
        public User(string accountName)
            : this()
        {
            _accountName = accountName;
        }

        /// <summary>
        /// Gets or sets users id.
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id 
        {
            get
            {
                return _id;
            }

            // We need it for DB mapping.
            set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Gets or sets an account name of user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember]
        public string AccountName
        {
            get
            {
                return _accountName;
            }

            set
            {
                _accountName = value;
            }
        }

        /// <summary>
        /// Gets or sets the hash of entered password.
        /// </summary>
        [DataMember]
        public string PasswordHash
        {
            get
            {
                return _passwordHash;
            }

            set
            {
                _passwordHash = value;
            } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether user is an administrator or not.
        /// </summary>
        [DataMember]
        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }

            set
            {
                _isAdmin = value;
            }
        }

        /// <summary>
        /// Gets or sets a personal info about user.
        /// </summary>
        [DataMember]
        public UserProfile Profile
        {
            get
            {
                return _profile;
            }

            set
            {
                _profile = value;
            }
        }

        /// <summary>
        /// Gets the collection of users contacts.
        /// </summary>
        [DataMember]
        [NotMapped]
        public FriendsCollection Friends
        {
            get
            {
                return _friends;
            }
        }
    }
}