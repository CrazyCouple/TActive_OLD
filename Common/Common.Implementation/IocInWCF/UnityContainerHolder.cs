// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using Common.Implementation.Extensions;
using Microsoft.Practices.Unity;

namespace Common.Implementation.IocInWCF
{
    /// <summary>
    /// Singleton which provides a Unity container in places where ID is impossible.
    /// </summary>
    public static class UnityContainerHolder
    {
        private static UnityContainer _container;

        /// <summary>
        /// Gets unity container.
        /// </summary>
        public static UnityContainer UnityContainer
        {
            get
            {
                if (_container == null)
                {
                    throw new InvalidOperationException(string.Format("The '{0}' should be initialized first!", typeof(UnityContainerHolder).Name));
                }

                return _container;
            }
        }

        /// <summary>
        /// Initializes <see cref="UnityContainerHolder"/> with Unity container.
        /// </summary>
        public static void Initialize(UnityContainer container)
        {
            container.ValidateNull("container");

            _container = container;
        }
    }
}