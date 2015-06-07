// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Common.Implementation.Extensions;
using NUnit.Framework;

namespace Common.Implementation.Tests.Extensions
{
    [TestFixture]
    public class CollectionExtensionsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRange_SourceIsNull_Throws()
        {
            ICollection<int> source = null;
            var input = new List<int>();

            source.AddRange(input);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AddRange_InputIsNull_Throws()
        {
            var source = new Collection<int>();
            IEnumerable<int> input = null;

            source.AddRange(input);
        }

        [Test]
        public void AddRange_Test()
        {
            var source = new Collection<int>();
            var input = new List<int> { 1, 2, 3, 4, 5 };
            
            source.AddRange(input);

            Assert.AreEqual(input, source);
        }
    }
}
