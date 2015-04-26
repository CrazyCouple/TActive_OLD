// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Configuration;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using DataBaseRepositories;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using NLog;

namespace Server.Console
{
    /// <summary>
    /// Server entry point.
    /// </summary>
    public static class Program
    {
        private const string UnityContainerFile = "Unity.config";

        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Server entry point.
        /// </summary>
        public static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TActiveContext>());

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            using (var container = CreateUnityContainer())
            {
            }

            LogManager.Flush();
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

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal("Unhandled exception: {0}", e.ExceptionObject);

            LogManager.Flush();
        }
    }
}
