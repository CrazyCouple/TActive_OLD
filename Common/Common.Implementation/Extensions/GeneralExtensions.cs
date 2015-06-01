// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Implementation.Extensions
{
    /// <summary>
    /// Provides a set of static methods to extend functionality of Generic types.
    /// </summary>
    public static class GeneralExtensions
    {
        /// <summary>
        /// Validates if input is not null. If it is null - throws.
        /// </summary>
        /// <typeparam name="T">Type of the parameter under test.</typeparam>
        /// <param name="obj">Parameter under test.</param>
        /// <param name="name">The name of the parameter that is under test.</param>
        public static void ValidateNull<T>(this T obj, string name) 
            where T : class
        {
            if (obj == default(T))
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}
