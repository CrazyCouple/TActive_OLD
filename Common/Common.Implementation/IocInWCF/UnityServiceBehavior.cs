// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;
using Microsoft.Practices.Unity;

namespace Common.Implementation.IocInWCF
{
    /// <summary>
    /// The default behavior uses a standard InstanceProvider, to inform WCF that we should use our UnityInstanceProvider we should to implement our own behavior.
    /// </summary>
    public class UnityServiceBehavior : IServiceBehavior
    {
        private readonly UnityContainer _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnityServiceBehavior"/> class.
        /// </summary>
        /// <param name="container">The IoC container.</param>
        public UnityServiceBehavior(UnityContainer container)
        {
            _container = container;
        }

        /// <summary>
        /// Provides the ability to inspect the service host and the service description
        /// to confirm that the service can run successfully.
        /// </summary>
        /// <param name="serviceDescription">The service description.</param>
        /// <param name="serviceHostBase">The service host that is currently being constructed.</param>
        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            if (serviceDescription == null)
            {
                throw new ArgumentNullException("serviceDescription");
            }

            if (serviceHostBase == null)
            {
                throw new ArgumentNullException("serviceHostBase");
            }
        }

        /// <summary>
        /// Provides the ability to pass custom data to binding elements to support the
        /// contract implementation.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host of the service.</param>
        /// <param name="endpoints">The service endpoints.</param>
        /// <param name="bindingParameters">Custom objects to which binding elements have access.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Provides the ability to change run-time property values or insert custom
        /// extension objects such as error handlers, message or parameter interceptors,
        /// security extensions, and other custom extension objects.
        /// </summary>
        /// <param name="serviceDescription">The service description of the service.</param>
        /// <param name="serviceHostBase">The host that is currently being built.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
            Validate(serviceDescription, serviceHostBase);

            foreach (var cdb in serviceHostBase.ChannelDispatchers)
            {
                var cd = cdb as ChannelDispatcher;

                if (cd != null)
                {
                    foreach (var ed in cd.Endpoints)
                    {
                        var serviceType = serviceDescription.ServiceType;
                        ed.DispatchRuntime.InstanceProvider = new UnityInstanceProvider(_container, serviceType);
                    }
                }
            }
        }
    }
}