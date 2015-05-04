// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public string AccountName { get; set; }

        /// <summary>
        /// Gets or sets a users Birthday.
        /// </summary>
        [DataMember]
        public DateTime Birthday { get; set; }
    }
}