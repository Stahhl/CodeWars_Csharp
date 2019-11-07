using System;
using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
    class FindTheUnknownDigit
    {
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
                        (num1[0] == '?' && num1[1] == '?') || 
                        (num2[0] == '?' && num2[1] == '?') || 
                        (num3[0] == '?' && num3[1] == '?')))
                        continue;

                    //1x11=11
                    //test is wrong ?:<
                    //1 -> skip
                    //if (i == 1)
                    //    continue;


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
