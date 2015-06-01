// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.Collections.Generic;

namespace Common.Implementation.Extensions
{
    /// <summary>
    /// Provides a set of static methods for querying objects that implement <see cref="ICollection{T}"/>.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Adds a set of elements to the source collection.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of source and input.</typeparam>
        /// <param name="source">An <see cref="ICollection{T}"/> to which a set of new elements should be added.</param>
        /// <param name="input">A set of new elements which will be added to the source collection.</param>
        public static void AddRange<TSource>(this ICollection<TSource> source, IEnumerable<TSource> input)
        {
            source.ValidateNull("source");
            input.ValidateNull("input");

            input.ForEach(source.Add);
        }
    }
}
