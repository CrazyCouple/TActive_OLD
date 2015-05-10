// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace Server.Implementation
{
    /// <summary>
    /// Holds unity container.
    /// </summary>
    public static class UnityContainerHolder
    {
        private const string UnityContainerFile = "Unity.config";
        private static UnityContainer _container;

        /// <summary>
        /// Gets unity container.
        /// </summary>
        public static UnityContainer UnityContainer
        {
            get { return _container ?? (_container = CreateUnityContainer()); }
        }

        [SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope", Justification = "The container must be disposed in external code.")]
        private static UnityContainer CreateUnityContainer()
        {
            var fileMap = new ExeConfigurationFileMap
            {
                ExeConfigFilename = UnityContainerFile
            };

            var configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");

            var container = new UnityContainer();
            container.LoadConfiguration(unitySection);

            return container;
        }
    }
}