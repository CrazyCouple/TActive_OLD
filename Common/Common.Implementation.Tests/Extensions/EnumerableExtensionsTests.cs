// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using System.Collections.Generic;
using Common.Implementation.Extensions;
using NUnit.Framework;

namespace Common.Implementation.Tests.Extensions
{
    [TestFixture]
    public class EnumerableExtensionsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForEach_SourceIsNull_Throws()
        {
            IEnumerable<int> source = null;

            source.ForEach(x => { x++; });
        }
     
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ForEach_ActionIsNull_Throws()
        {
            IEnumerable<int> source = new List<int>();

            source.ForEach(null);
        }

        [Test]
        public void ForEach_Test()
        {
            var counter = 1;
            IEnumerable<int> source = new List<int> { 1, 2, 3, 4, 5 };

            source.ForEach(x => Assert.AreEqual(counter++, x));
        }
    }
}
