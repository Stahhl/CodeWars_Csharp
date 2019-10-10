using System;
using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
    class Playing_with_digits
    {
        public static long digPow(int n, int p)
        {
            // your code
            var intSplit = n.ToString().Select(x => int.Parse(x.ToString()));
            int sum = 0;

            foreach (var i in intSplit)
            {
                sum += (int)Math.Pow(i, p);
                p++;
            }

            //n * k = sum
            //k = sum / n
            var k = (double)sum / (double)n;

            if (k % 1 == 0)
            {
                return (int)k;
            }

            return -1;
        }

        [TestFixture]
        public class DigPowTests
        {
            [Test]
            public void Test1()
            {
                Assert.AreEqual(1, digPow(89, 1));
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual(-1, digPow(92, 1));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual(51, digPow(46288, 3));
            }
        }
    }
}
