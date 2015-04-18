// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using NLog;

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
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            LogManager.Flush();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Fatal("Unhandled exception: {0}", e.ExceptionObject);

            LogManager.Flush();
        }
    }
}
