// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Common.DataContracts
{
    /// <summary>
    /// The collection of users contacts.
    /// </summary>
    [CollectionDataContract]
    public class FriendsCollection : Collection<User>
    {
    }
}