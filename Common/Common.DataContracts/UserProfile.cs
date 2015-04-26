// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.DataContracts
{
    /// <summary>
    /// Represents a personal info about user.
    /// </summary>
    [DataContract]
    public class UserProfile
    {
        /// <summary>
        /// Gets or sets an account name of user.
        /// </summary>
        /// <value>The name of the user.</value>
        [DataMember]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets the hash of entered password.
        /// </summary>
        /// <value>The users password hash.</value>
        [DataMember]
        public int PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets a users age.
        /// </summary>
        /// <value>The users age.</value>
        [DataMember]
        public int Age { get; set; }
    }
}