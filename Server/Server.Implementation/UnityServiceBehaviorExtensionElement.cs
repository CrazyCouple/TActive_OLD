// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.ServiceModel.Configuration;
using Common.Implementation.IocInWCF;

namespace Server.Implementation
{
    /// <summary>
    /// Configuration element.
    /// </summary>
    public class UnityServiceBehaviorExtensionElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets type of UnityServiceBehavior.
        /// </summary>
        public override Type BehaviorType
        {
            get { return typeof(UnityServiceBehavior); }
        }

        /// <summary>
        /// Creates and returns UnityServiceBehavior.
        /// </summary>
        protected override object CreateBehavior()
        {
            return new UnityServiceBehavior(UnityContainerHolder.UnityContainer);
        }
    }
}