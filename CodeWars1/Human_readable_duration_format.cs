using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeWars1
{
    //https://www.codewars.com/kata/52742f58faf5485cae000b9a/train/csharp
    class Human_readable_duration_format
    {
        private static int sec = 0;
        private static int min = 0;
        private static int hrs = 0;
        private static int dys = 0;
        private static int yrs = 0;


        public static string FormatDuration(int seconds)
        {
            var ts = new TimeSpan(0, 0, seconds);

            sec = ts.Seconds;
            min = ts.Minutes;
            hrs = ts.Hours;
            dys = ts.Days;
            yrs = 0;

            if(dys >= 365)
            {
                yrs = dys / 365;
                dys -= yrs * 365;
            }

            return seconds == 0 ? "now" : FormatString();
        }
        private static string FormatString()
        {
            List<string> list = new List<string>();
            string result = string.Empty;

            if(yrs > 0)
            {
                list.Add(yrs == 1 ? $"{yrs} year" : $"{yrs} years");
            }
            if (dys > 0)
            {
                list.Add(dys == 1 ? $"{dys} day" : $"{dys} days");
            }
            if (hrs > 0)
            {
                list.Add(hrs == 1 ? $"{hrs} hour" : $"{hrs} hours");
            }
            if (min > 0)
            {
                list.Add(min == 1 ? $"{min} minute" : $"{min} minutes");
            }
            if (sec > 0)
            {
                list.Add(sec == 1 ? $"{sec} second" : $"{sec} seconds");
            }

            for (int i = 0; i < list.Count - 1; i++)
            {
                if(i == list.Count - 2)
                {
                    list[i] += " and ";
                }
                else
                {
                    list[i] += ", ";
                }
            }

            foreach (var str in list)
            {
                result += str;
            }

            return result;
        }
        [TestFixture]
        public class Tests
        {
            [Test]
            public void Test1()
            {
                Assert.AreEqual("now", FormatDuration(0));
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual("1 second", FormatDuration(1));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual("1 minute and 2 seconds", FormatDuration(62));
            }
            [Test]
            public void Test4()
            {
                Assert.AreEqual("2 minutes", FormatDuration(120));
            }
            [Test]
            public void Test5()
            {
                Assert.AreEqual("1 hour, 1 minute and 2 seconds", FormatDuration(3662));
            }
            [Test]
            public void Test6()
            {
                Assert.AreEqual("182 days, 1 hour, 44 minutes and 40 seconds", FormatDuration(15731080));
            }
            [Test]
            public void Test7()
            {
                Assert.AreEqual("4 years, 68 days, 3 hours and 4 minutes", FormatDuration(132030240));
            }
            [Test]
            public void Test8()
            {
                Assert.AreEqual("6 years, 192 days, 13 hours, 3 minutes and 54 seconds", FormatDuration(205851834));
            }
            [Test]
            public void Test9()
            {
                Assert.AreEqual("8 years, 12 days, 13 hours, 41 minutes and 1 second", FormatDuration(253374061));
            }
            [Test]
            public void Test10()
            {
                Assert.AreEqual("7 years, 246 days, 15 hours, 32 minutes and 54 seconds", FormatDuration(242062374));
            }
            [Test]
            public void Test11()
            {
                Assert.AreEqual("3 years, 85 days, 1 hour, 9 minutes and 26 seconds", FormatDuration(101956166));
            }
            [Test]
            public void Test12()
            {
                Assert.AreEqual("1 year, 19 days, 18 hours, 19 minutes and 46 seconds", FormatDuration(33243586));
            }

        }
    }
}
