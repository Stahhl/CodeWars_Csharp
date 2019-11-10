using System;
using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
    class FindTheUnknownDigit
    {
        //https://www.codewars.com/kata/find-the-unknown-digit/train/csharp
        public static int SolveExpression(string expression)
        {
            //Write code to determine the missing digit or unknown rune
            //Expression will always be in the form
            //(number)[opperator](number)=(number)
            //Unknown digit will not be the same as any other digits used in expression

            char[] opperators = new char[] { '+', '-', '*', '/' };
            bool hasOpp = false;
            bool hasEqual = false;

            string num1 = string.Empty;
            string opp = string.Empty;
            string num2 = string.Empty;
            string num3 = string.Empty;

            for (int i = 0; i < expression.Length; i++)
            {
                if (opperators.Contains(expression[i]) && i > 0 && hasOpp == false)
                {
                    opp = expression[i].ToString();
                    hasOpp = true;
                }
                else if (expression[i] == '=')
                {
                    hasEqual = true;
                }
                else
                {
                    if (hasOpp == false && hasEqual == false)
                        num1 += expression[i];
                    if (hasOpp == true && hasEqual == false)
                        num2 += expression[i];
                    if (hasOpp == true && hasEqual == true)
                        num3 += expression[i];
                }
            }

            return EvaluateExpression(num1, num2, num3, opp);
        }
        private static int EvaluateExpression(string num1, string num2, string num3, string opp)
        {
            try
            {
                for (int i = 0; i < 10; i++)
                {
                    //00 -> 0
                    //leading zero -> skip
                    if(i == 0 && (
                        (LeadingZero(num1) == false) || 
                        (LeadingZero(num2) == false) || 
                        (LeadingZero(num3) == false)))
                        continue;

                    //11x?=11
                    //cant be 1 because that digit is already in the equation
                    if (ExistingNumber(num1, i) == false ||
                        ExistingNumber(num2, i) == false ||
                        ExistingNumber(num3, i) == false)
                        continue;

                    int x = int.Parse(num1.Replace("?", i.ToString()));
                    int y = int.Parse(num2.Replace("?", i.ToString()));
                    int z = int.Parse(num3.Replace("?", i.ToString()));


                    if (EvaluateOpperator(x, y, opp) == z)
                        return i;
                }

                return -1;
            }
            catch
            {
                return -1;
            }
        }
        private static bool ExistingNumber(string num, int i)
        {
            foreach (var c in num)
            {
                if(int.TryParse(c.ToString(), out int result) == true &&
                    result == i)
                {
                    return false;
                }
            }

            return true;
        }
        private static bool LeadingZero(string num)
        {
            if(num.Length > 1 &&
                num[0] == '?' &&
                num[1] == '?')
            {
                return false;
            }

            return true;
        }
        private static int EvaluateOpperator(int x, int y, string opp)
        {
            return opp == "+" ? x + y :
                opp == "-" ? x - y :
                opp == "*" ? x * y :
                opp == "/" ? x / y :
                -1;
        }
        [TestFixture]
        public class RunesTest
        {
            //[Test]
            //public void Test10()
            //{
            //    Assert.AreEqual(1, SolveExpression("1+0=?"), "Answer for expression '1+0=?' ");
            //}
            //[Test]
            //public void Test9()
            //{
            //    Assert.AreEqual(0, SolveExpression("0+0=?"), "Answer for expression '0+0=?' ");
            //}
            [Test]
            public void Test1()
            {
                Assert.AreEqual(2, SolveExpression("1+1=?"), "Answer for expression '1+1=?' ");
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual(6, SolveExpression("123*45?=5?088"), "Answer for expression '123*45?=5?088' ");
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual(0, SolveExpression("-5?*-1=5?"), "Answer for expression '-5?*-1=5?' ");
            }
            [Test]
            public void Test4()
            {
                Assert.AreEqual(-1, SolveExpression("19--45=5?"), "Answer for expression '19--45=5?' ");
            }
            [Test]
            public void Test5()
            {
                Assert.AreEqual(5, SolveExpression("??*??=302?"), "Answer for expression '??*??=302?' ");
            }
            [Test]
            public void Test6()
            {
                Assert.AreEqual(2, SolveExpression("?*11=??"), "Answer for expression '?*11=??' ");
            }
            [Test]
            public void Test7()
            {
                Assert.AreEqual(2, SolveExpression("??*1=??"), "Answer for expression '??*1=??' ");
            }
            [Test]
            public void Test8()
            {
                Assert.AreEqual(-1, SolveExpression("??+??=??"), "Answer for expression '??+??=??' ");
            }
        }
    }
}
