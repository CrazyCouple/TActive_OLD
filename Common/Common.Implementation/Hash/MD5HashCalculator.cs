// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Security.Cryptography;
using Common.Implementation.Extensions;

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Calculates MD5 hash from string or byte array.
    /// </summary>
    public sealed class MD5HashCalculator : IHashCalculator
    {
        private readonly MD5 _md5Algorithm;
        private bool _disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="MD5HashCalculator"/> class.
        /// </summary>
        public MD5HashCalculator()
        {
            _md5Algorithm = new MD5Cng();
        }

        /// <summary>
        /// Creates hash from byte array.
        /// </summary>
        /// <param name="buffer">Input data.</param>
        public IHash Compute(byte[] buffer)
        {
            ThrowIfDisposed();

            buffer.ValidateNull("buffer");

            return new MD5Hash(_md5Algorithm.ComputeHash(buffer));
        }

        /// <summary>
        /// Creates hash from string.
        /// </summary>
        /// <param name="input">Input data.</param>
        public IHash Compute(string input)
        {
            ThrowIfDisposed();

            input.ValidateEmpty("input");

            var buffer = System.Text.Encoding.ASCII.GetBytes(input);
            return new MD5Hash(_md5Algorithm.ComputeHash(buffer));
        }

        /// <summary>
        /// Disposes the object.
        /// </summary>
        public void Dispose()
        {
            _md5Algorithm.Dispose();

            _disposed = true;
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}