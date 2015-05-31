// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Creates hash from string or byte array.
    /// Provides a hash manipulation methods. 
    /// </summary>
    public interface IHashCalculator : IDisposable
    {
        /// <summary>
        /// Creates hash from byte array.
        /// </summary>
        /// <param name="buffer">Input data.</param>
        /// <returns></returns>
        IHash Compute(byte[] buffer);

        /// <summary>
        /// Creates hash from string.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns></returns>
        IHash Compute(string input);
    }
}