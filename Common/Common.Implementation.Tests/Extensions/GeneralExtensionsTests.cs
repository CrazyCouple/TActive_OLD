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
    public class GeneralExtensionsTests
    {
        [Test]
        public void ValidateNull_ParameterIsNull_Throws()
        {
            Assert.Throws<ArgumentNullException>(() => ((object)null).ValidateNull("object"));
            Assert.Throws<ArgumentNullException>(() => ((Tuple<int>)null).ValidateNull("Tuple<int>"));
            Assert.Throws<ArgumentNullException>(() => ((string)null).ValidateNull("string"));
        }

        [Test]
        public void ValidateNull_ParameterNotNull_DoNothing()
        {
            (new object()).ValidateNull("object");
            (new Tuple<int>(0)).ValidateNull("Tuple<int>");
            "Some string".ValidateNull("string");
        }
    }
}
