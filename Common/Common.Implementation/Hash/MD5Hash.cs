// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Globalization;
using System.Linq;
using Common.Implementation.Extensions;

namespace Common.Implementation.Hash
{
    /// <summary>
    /// Represents MD5 hash result.
    /// </summary>
    public sealed class MD5Hash : IHash
    {
        private const int MD5HahsLength = 16;

        private readonly byte[] _hash;

        /// <summary>
        /// Initializes a new instance of the <see cref="MD5Hash"/> class.
        /// </summary>
        /// <param name="hash">Byte representation of the MD5 hash.</param>
        public MD5Hash(byte[] hash)
        {
            hash.ValidateNull("hash");

            if (hash.Length != MD5HahsLength)
            {
                throw new ArgumentException(string.Format("Incorect MD5 hash length. Must be '{0}' but was '{1}'.", MD5HahsLength, hash.Length));
            }

            _hash = hash;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MD5Hash"/> class.
        /// </summary>
        /// <param name="hash">String representation of the MD5 hash.</param>
        public MD5Hash(string hash)
        {
            hash.ValidateNullOrEmpty("hash");

            if (hash.Length != MD5HahsLength * 2)
            {
                throw new ArgumentException(string.Format("Incorect length of the MD5 hash sting representation. Must be '{0}' but was '{1}'.", MD5HahsLength * 2, hash.Length));
            }

            _hash = new byte[MD5HahsLength];

            for (var i = 0; i < (MD5HahsLength * 2); i += 2)
            {
                _hash[i / 2] = byte.Parse(string.Format("{0}{1}", hash[i], hash[i + 1]), NumberStyles.HexNumber);
            }
        }

        /// <summary>
        /// Gets native representation of the hash.
        /// </summary>
        public byte[] Hash
        {
            get { return _hash; }
        }

        /// <summary>
        /// Converts hash bytes representation to a string representation.
        /// </summary>
        public override string ToString()
        {
            return string.Join(string.Empty, _hash.Select(x => string.Format("{0:x2}", x)));
        }
    }
}
