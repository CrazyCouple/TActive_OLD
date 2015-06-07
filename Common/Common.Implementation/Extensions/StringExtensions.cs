// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Implementation.Extensions
{
    /// <summary>
    /// Provides a set of static methods to extend functionality of the <see cref="System.String"/>.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Throws if a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="source">String under test.</param>
        /// <param name="name">The name of the parameter that is under test.</param>
        /// <exception cref="ArgumentNullException">In case if the string is NULL.</exception>
        /// <exception cref="ArgumentException">In case if the string is empty or consists only of white-space characters.</exception>
        public static void ValidateNullOrEmpty(this string source, string name)
        {
            source.ValidateNull(name);

            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException("String shouldn't be empty!", name);
            }
        }
    }
}
