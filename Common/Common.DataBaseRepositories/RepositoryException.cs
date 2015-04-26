// // <copyright company="Tarcha & Ivchenko Company">
// //      Copyright (c) 2015, All Right Reserved
// // </copyright>
// // <author>Ivan Ivchenko</author>
// // <author>Myroslava Tarcha</author>

using System;
using System.Runtime.Serialization;

namespace DataBaseRepositories
{
    /// <summary>
    /// Wraps data stores exception.
    /// </summary>
    [Serializable]
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        public RepositoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RepositoryException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerExeption">The inner exception</param>
        public RepositoryException(string message, Exception innerExeption)
            : base(message, innerExeption)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="info"> Serialization info.</param>
        /// <param name="context">Streaming context.</param>
        protected RepositoryException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}