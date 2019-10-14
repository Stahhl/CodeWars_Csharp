using System;
using System.Collections;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;

namespace CodeWars1
{
    //https://www.codewars.com/kata/valid-braces/train/csharp
    class ValidBraces
    {
        public static bool validBraces(String braces)
        {
            var list = braces.ToList();
            var startBrace = new char[] { '(', '{', '[' };
            var closeBrace = new char[] { ')', '}', ']' };
            var starting = list.Where(x => startBrace.Contains(x));
            var closing = list.Where(x => closeBrace.Contains(x));

            if (braces.Length % 2 != 0 || (starting.Count() != closing.Count()))
                return false;

            for (int a = 0; a < list.Count() - 1; a++)
            {
                var current = list[a];
                var next = list[a + 1];

                if(startBrace.Contains(current) || closeBrace.Contains(next))
                {
                    var currentIndex = Array.IndexOf(startBrace, current);
                    var nextIndex = Array.IndexOf(closeBrace, next);

                    if(currentIndex == nextIndex)
                    {
                        list.Remove(current);
                        list.Remove(next);

                        a = -1;
                    }
                }
            }

            return list.Count == 0 ? true : false;
        }

        [TestFixture]
        public class BraceTests
        {

            [Test]
            public void Test1()
            {
                Assert.AreEqual(true, validBraces("()"));
            }
            [Test]
            public void Test2()
            {

                Assert.AreEqual(false, validBraces("[(])"));
            }
            [Test]
            public void Test3()
            {

                Assert.AreEqual(true, validBraces("(){}[]"));
            }
            [Test]
            public void Test4()
            {

                Assert.AreEqual(true, validBraces("([{}])"));
            }
            [Test]
            public void Test5()
            {

                Assert.AreEqual(false, validBraces("(}"));
            }
            [Test]
            public void Test6()
            {

                Assert.AreEqual(false, validBraces("[(])"));
            }
            [Test]
            public void Test7()
            {

                Assert.AreEqual(false, validBraces("[({})](]"));
            }
            [Test]
            public void Test8()
            {
                Assert.AreEqual(true, validBraces("((((((((((((((((((((()))))))))))))))))))))"));
            }
            [Test]
            public void Test9()
            {
                Assert.AreEqual(true, validBraces("(())[]"));
            }
        }
    }
}
