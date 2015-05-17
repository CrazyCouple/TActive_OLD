// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Data.Entity;
using System.ServiceModel;
using System.ServiceModel.Description;
using Common.DatabaseRepositories;
using Common.Implementation.IocInWCF;
using Microsoft.Practices.Unity;
using NLog;
using Server.Implementation;
using Server.Services;

namespace Server.Console
{
    /// <summary>
    /// Server entry point.
    /// </summary>
    public static class Program
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Server entry point.
        /// </summary>
        public static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<TActiveContext>());

            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            var container = UnityContainerHolder.UnityContainer;
            container.RegisterType<IServiceBehavior, UnityServiceBehavior>();

            using (var host = new ServiceHost(typeof(RegistrationService)))
            {
                host.Open();

                System.Console.ReadLine();
            }

            LogManager.Flush();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal("Unhandled exception: {0}", e.ExceptionObject);

            LogManager.Flush();
        }
    }
}
