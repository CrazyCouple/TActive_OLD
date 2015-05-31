// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace Common.Implementation.IocInWCF
{
    /// <summary>
    /// By default WCF requires that service implementation classes have a constructor without parameters. 
    /// For the services creation is responsible the IInstanceProvider, but the standard does not know how to do DI, so we need to implement their own.
    /// For this we add the following class.
    /// </summary>
    public class UnityInstanceProvider : IInstanceProvider
    {
        private readonly UnityContainer _container;
        private readonly Type _serviceType;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityInstanceProvider"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        /// <param name="serviceType">The service type.</param>
        public UnityInstanceProvider(UnityContainer container, Type serviceType)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            if (serviceType == null)
            {
                throw new ArgumentNullException("serviceType");
            }

            _serviceType = serviceType;
            _container = container;
        }

        /// <summary>
        /// Returns a service object given the specified System.ServiceModel.InstanceContext object.
        /// </summary>
        /// <param name="instanceContext">The current System.ServiceModel.InstanceContext object.</param>
        public object GetInstance(InstanceContext instanceContext)
        {
            return _container.Resolve(_serviceType);
        }

        /// <summary>
        /// Returns a service object given the specified System.ServiceModel.InstanceContext object.
        /// </summary>
        /// <param name="instanceContext">The current System.ServiceModel.InstanceContext object.</param>
        /// <param name="message">The message that triggered the creation of a service object.</param>
        public object GetInstance(InstanceContext instanceContext, Message message)
        {
            return GetInstance(instanceContext);
        }

        /// <summary>
        /// Called when an System.ServiceModel.InstanceContext object recycles a service object.
        /// </summary>
        /// <param name="instanceContext">The service's instance context.</param>
        /// <param name="instance">The service object to be recycled.</param>
        public void ReleaseInstance(InstanceContext instanceContext, object instance)
        {
            _container.Teardown(instance);
        }
    }
}