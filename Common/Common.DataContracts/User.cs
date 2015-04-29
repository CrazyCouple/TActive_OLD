// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
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
        public User(UserProfile profile)
        {
            _profile = profile;
            _friends = new FriendsCollection();
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Gets users id.
        /// </summary>
        [Key]
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets a personal info about user.
        /// </summary>
        /// <value>The users profile.</value>
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
        /// <value>The users friends.</value>
        [DataMember]
        [NotMapped]
        public FriendsCollection Friends
        {
            get
            {
                return _friends;
            }
        }

        /// <summary>
        /// Adds a new contact to users friends.
        /// </summary>
        /// <param name="friend">Other user.</param>
        public void AddFriend(User friend)
        {
            _friends.Add(friend);
        }
    }
}