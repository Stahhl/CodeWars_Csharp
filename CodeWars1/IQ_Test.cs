using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars1
{
    [TestFixture]
    public class IQTests
    {
        public static int Test(string numbers)
        {
            //Your code is here...
            var list = numbers.Split(' ').Select(x => int.Parse(x.ToString())).ToList();
            var evens = new List<int>();
            var odds = new List<int>();

            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i] % 2 == 0)
                    evens.Add(i + 1);
                if (list[i] % 2 != 0)
                    odds.Add(i + 1);
            }

            if (evens.Count < odds.Count)
                return evens[evens.Count - 1];
            else
                return odds[odds.Count - 1];
        }

        [Test]
        public void Test1()
        {
            Assert.AreEqual(3, Test("2 4 7 8 10"));
        }

        [Test]
        public void Test2()
        {
            Assert.AreEqual(1, Test("1 2 2"));
        }
    }
}
