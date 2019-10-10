using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace CodeWars1
{
    class Is_a_number_prime
    {
        //https://stackoverflow.com/questions/15743192/check-if-number-is-prime-number/15743238#15743238
        public static bool IsPrime(int n)
        {
            if (n == 2) return true;
            if (n <= 1 || n % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(n));

            for (int i = 3; i <= boundary; i += 2)
                if (n % i == 0)
                    return false;

            return true;
        }

        [TestFixture]
        public class SolutionTest
        {
            private static IEnumerable<TestCaseData> sampleTestCases
            {
                get
                {
                    yield return new TestCaseData(0).Returns(false);
                    yield return new TestCaseData(1).Returns(false);
                    yield return new TestCaseData(2).Returns(true);
                }
            }

            [Test, TestCaseSource("sampleTestCases")]
            public bool SampleTest(int n) => IsPrime(n);
        }
    }
}
