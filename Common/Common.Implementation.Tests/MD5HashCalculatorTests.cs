// <copyright company="Tarcha & Ivchenko Company">
//      Copyright (c) 2015, All Right Reserved
// </copyright>
// <author>Ivan Ivchenko</author>
// <author>Myroslava Tarcha</author>

using System;
using Common.Implementation.Hash;
using NUnit.Framework;

namespace Common.Implementation.Tests
{
    [TestFixture]
    public class MD5HashCalculatorTests
    {
        [Test]
        [Ignore("The test made only for manual purposes.")]
        public void Compute_Buffer()
        {
            using (var calculator = new MD5HashCalculator())
            {
                var buffer = new byte[] { 1, 2, 3, 4, 5 };
                
                var hash = calculator.Compute(buffer);
                var sameHash = new MD5Hash(hash.ToString());

                Console.WriteLine("Hash buff: {0}; Hash to string: {1}", string.Join(";", hash.Hash), hash);
                Console.WriteLine("Same Hash buff: {0}; Same Hash to string: {1}", string.Join(";", sameHash.Hash), sameHash);
            }
        }

        [TestCase("Hello world!", Result = "86fb269d190d2c85f6e0468ceca42a20")]
        [TestCase("Tarcha Maladez", Result = "bb526c6510b38dfe7522d1610690fbc9")]
        [TestCase("Ivchenko kakashka", Result = "2d7a9fd2d3220464e65a68d70463f8ff")]
        public string Calculate_InputString(string input)
        {
            using (var calculator = new MD5HashCalculator())
            {
                return calculator.Compute(input).ToString();
            }
        }
    }
}
