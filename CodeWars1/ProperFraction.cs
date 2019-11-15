using NUnit.Framework;
using System;

namespace CodeWars1
{
    //https://www.codewars.com/kata/55b7bb74a0256d4467000070/train/csharp
    public class ProperFractionsSolution
    {
        public static long ProperFractions(long n)
        {
            //https://rosettacode.org/wiki/Totient_function#C.23
            long hardCoded = HardCodedReturns(n);

            if (hardCoded != -1)
                return hardCoded;

            long result = n;
            long boundary = Convert.ToInt64(Math.Sqrt(n) + 1);

            for (long i = 2; i <= boundary; i++)
            {
                if (n % i == 0)
                {
                    while (n % i == 0)
                    {
                        n /= i;
                    }
                    result -= result / i;
                }
            }
            if (n > 1)
            {
                result -= result / n;
            }

            return result;
        }
        private static long HardCodedReturns(long n)
        {
            if (n == 1)
                return 0;
            if (n > 1 && n < 3)
                return 1;
            if (n == 3)
                return 2;
            if (IsPrime(n) == true)
                return n - 1;

            return -1;
        }
        private static bool IsPrime(long n)
        {
            if (n == 2)
                return true;
            if (n <= 1 || n % 2 == 0)
                return false;

            var boundary = (long)Math.Floor(Math.Sqrt(n));

            for (long i = 3; i <= boundary; i += 2)
                if (n % i == 0)
                    return false;

            return true;
        }
    }
    public class ProperFractionTests
    {
        [Test]
        public void Test1()
        {
            Assert.AreEqual(0, ProperFractionsSolution.ProperFractions(1));
        }
        [Test]
        public void Test2()
        {
            Assert.AreEqual(1, ProperFractionsSolution.ProperFractions(2));
        }
        [Test]
        public void Test3()
        {
            Assert.AreEqual(4, ProperFractionsSolution.ProperFractions(5));
        }
        [Test]
        public void Test4()
        {
            Assert.AreEqual(8, ProperFractionsSolution.ProperFractions(15));
        }
        [Test]
        public void Test5()
        {
            Assert.AreEqual(20, ProperFractionsSolution.ProperFractions(25));
        }
        [Test]
        public void Test6()
        {
            Assert.AreEqual(2147483646, ProperFractionsSolution.ProperFractions(2147483647));
        }
        [Test]
        public void Test7()
        {
            Assert.AreEqual(7713001620195508224, ProperFractionsSolution.ProperFractions(9223372036854775807));
        }
    }
}
