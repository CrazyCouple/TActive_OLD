// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using Common.Implementation.Extensions;
using NUnit.Framework;

namespace Common.Implementation.Tests.Extensions
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ValidateEmpty_Null_Throws()
        {
            string input = null;

            input.ValidateNullOrEmpty("input");
        }

        [TestCase("")]
        [TestCase("  ")]
        [ExpectedException(typeof(ArgumentException))]
        public void ValidateEmpty_Empty_Throws(string input)
        {
            input.ValidateNullOrEmpty("input");
        }

        [TestCase("Hello")]
        [TestCase(" good job ")]
        public void ValidateEmpty_Test(string input)
        {
            input.ValidateNullOrEmpty("input");
        }
    }
}
