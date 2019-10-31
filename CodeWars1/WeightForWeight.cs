using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
    //https://www.codewars.com/kata/55c6126177c9441a570000cc/train/csharp
    class WeightForWeight
    {
        public class WeightSort
        {
            public static string orderWeight(string strng)
            {
                if (strng == string.Empty)
                    return string.Empty;

                var map = new List<KeyValuePair<long, long>>();
                string[] split = strng.Trim().Split(" ");

                foreach (var str in split)
                {
                    var list = new List<long>();
                    foreach (var c in str)
                    {
                        list.Add(int.Parse(c.ToString()));
                    }
                    //2000 = 2+0+0+0 = 2
                    map.Add(new KeyValuePair<long, long>(long.Parse(str), list.Sum()));
                }
                var ordered = map.OrderBy(x => x.Value).ThenBy(x => x.Key.ToString()).ToList();

                return string.Join(" ", ordered.Select(x => x.Key));
            }
        }
        [TestFixture]
        public class WeightSortTests
        {
            [Test]
            public void Test1()
            {
                Assert.AreEqual("2000 103 123 4444 99", WeightSort.orderWeight("103 123 4444 99 2000"));
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual("11 11 2000 10003 22 123 1234000 44444444 9999", WeightSort.orderWeight("2000 10003 1234000 44444444 9999 11 11 22 123"));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual("", WeightSort.orderWeight(""));
            }
        }
    }
}
