// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Creates hash from string or byte array.
    /// Provides a hash manipulation methods. 
    /// </summary>
    public interface IHashCalculator
    {
        /// <summary>
        /// Creates hash from byte array.
        /// </summary>
        /// <param name="buffer">Input data.</param>
        /// <returns></returns>
        byte[] ComputeHashFromBytes(byte[] buffer);

        /// <summary>
        /// Creates hash from string.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns></returns>
        byte[] ComputeHashFromString(string input);

        /// <summary>
        /// Converts hash to string.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        string StringRepresentation(byte[] hash);

        /// <summary>
        /// Converts hash to byte array.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        byte[] ByteArrayRepresentation(string hash);
    }
}