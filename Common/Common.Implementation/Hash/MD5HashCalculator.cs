// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System.Security.Cryptography;

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Creates hash from string or byte array.
    /// Provides a hash manipulation methods. 
    /// </summary>
    public class MD5HashCalculator : IHashCalculator
    {
        private readonly MD5 _md5Algorithm;

        /// <summary>
        /// Initializes a new instance of the <see cref="MD5HashCalculator"/> class.
        /// </summary>
        public MD5HashCalculator()
        {
            _md5Algorithm = MD5.Create();
        }

        /// <summary>
        /// Creates hash from byte array.
        /// </summary>
        /// <param name="buffer">Input data.</param>
        /// <returns></returns>
        public byte[] ComputeHashFromBytes(byte[] buffer)
        {
            return _md5Algorithm.ComputeHash(buffer);
        }

        /// <summary>
        /// Creates hash from string.
        /// </summary>
        /// <param name="input">Input data.</param>
        /// <returns></returns>
        public byte[] ComputeHashFromString(string input)
        {
            var buffer = GetBytes(input);
            return _md5Algorithm.ComputeHash(buffer);
        }

        /// <summary>
        /// Converts hash to string.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public string StringRepresentation(byte[] hash)
        {
            return GetString(hash);
        }

        /// <summary>
        /// Converts hash to byte array.
        /// </summary>
        /// <param name="hash">The hash.</param>
        /// <returns></returns>
        public byte[] ByteArrayRepresentation(string hash)
        {
            return GetBytes(hash);
        }

        private static byte[] GetBytes(string str)
        {
            var bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        private static string GetString(byte[] bytes)
        {
            var chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}