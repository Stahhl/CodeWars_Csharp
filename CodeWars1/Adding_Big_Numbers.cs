using System.Numerics;
using NUnit.Framework;

namespace CodeWars1
{
    //https://www.codewars.com/kata/adding-big-numbers/train/csharp
    class Adding_Big_Numbers
    {
        public static string Add(string a, string b)
        {
            BigInteger numA = BigInteger.Parse(a.Replace(" ", ""));
            BigInteger numB = BigInteger.Parse(b.Replace(" ", ""));

            BigInteger numC = numA + numB;

            string result = numC.ToString();

            return result;
        }
        [TestFixture]
        public class SolutionTest
        {
            //[Test]
            //public void Test1()
            //{
            //    Assert.AreEqual("6000000000000.5", Add("3000000000000.25", "3000000000000.25"));
            //}
            [Test]
            public void Test2()
            {
                Assert.AreEqual("5000000000", Add("2 500 000 000", "2 500 000 000"));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual("10000000000000", Add("5000000000000", "5000000000000"));
            }
            [Test]
            public void Test4()
            {
                string term = "528926754720183832841225229397433232250873290194333258971827";
                string sum = "1057853509440367665682450458794866464501746580388666517943654";

                Assert.AreEqual(sum, Add(term, term));
            }
        }
    }
}
