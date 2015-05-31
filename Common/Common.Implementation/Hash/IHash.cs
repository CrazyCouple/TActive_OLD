// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Represents hash.
    /// </summary>
    public interface IHash
    {
        /// <summary>
        /// Gets native representation of the hash.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays", Justification = "Hash representation should be always in byte array.")]
        byte[] Hash { get; }

        /// <summary>
        /// Converts hash bytes representation to a string representation.
        /// </summary>
        /// <returns></returns>
        string ToString();
    }
}
