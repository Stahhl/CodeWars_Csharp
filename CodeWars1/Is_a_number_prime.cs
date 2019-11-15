using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CodeWars1
{
    class Is_a_number_prime
    {
        //https://stackoverflow.com/questions/15743192/check-if-number-is-prime-number/15743238#15743238
        public static bool IsPrime(long n)
        {
            if (n == 2) return true;
            if (n <= 1 || n % 2 == 0) return false;

            var boundary = (long)Math.Floor(Math.Sqrt(n));

            for (long i = 3; i <= boundary; i += 2)
                if (n % i == 0)
                    return false;

            return true;
        }

        [TestFixture]
        public class SolutionTest
        {
            [Test]
            public void Test1()
            {
                Assert.AreEqual(false, IsPrime(0));
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual(false, IsPrime(1));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual(true, IsPrime(2));
            }
            [Test]
            public void Test4()
            {
                Assert.AreEqual(true, IsPrime(2147483647));
            }
            [Test]
            public void Test5()
            {
                Assert.AreEqual(false, IsPrime(9223372036854775807));
            }
        }
    }
}
