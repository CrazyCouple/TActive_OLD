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
        private string _passwordHash;
        private UserProfile _profile;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// This constructor we need for DB working.
        /// </summary>
        public User()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="profile">The personal users info.</param>
        /// <param name="id">Users id in DB.</param>
        public User(UserProfile profile, long id)
        {
            _profile = profile;
            _friends = new FriendsCollection();
            _id = id;
        }

        /// <summary>
        /// Gets users id.
        /// </summary>
        [Key]
        public long Id 
        {
            get
            {
                return _id;
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