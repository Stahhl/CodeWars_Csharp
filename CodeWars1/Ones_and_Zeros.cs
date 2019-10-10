using NUnit.Framework;
using System;

namespace CodeWars1
{
    //https://www.codewars.com/kata/ones-and-zeros/train/csharp
    class Ones_and_Zeros
    {
        public static int binaryArrayToNumber(int[] BinaryArray)
        {
            //Code here
            string s = string.Empty;

            foreach (var item in BinaryArray)
            {
                s += item.ToString();
            }

            return (int)Convert.ToInt64(s, 2);
        }
        [TestFixture]
        public class SolutionTest
        {
            int[] Test1 = new int[] { 0, 0, 0, 0 };
            int[] Test2 = new int[] { 1, 1, 1, 1 };
            int[] Test3 = new int[] { 0, 1, 1, 0 };
            int[] Test4 = new int[] { 0, 1, 0, 1 };

            [Test]
            public void BasicTesting()
            {
                Assert.AreEqual(0, binaryArrayToNumber(Test1));
                Assert.AreEqual(15, binaryArrayToNumber(Test2));
                Assert.AreEqual(6, binaryArrayToNumber(Test3));
                Assert.AreEqual(5, binaryArrayToNumber(Test4));
            }
        }
    }
}
