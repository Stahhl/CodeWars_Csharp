using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
    class Persistent_Bugger
    {
        public static int Persistence(long n)
        {
            // your code
            int current = (int)n;
            int loop = 0;
            while (current.ToString().Length > 1 && loop < 10)
            {
                loop++;

                var digits = current.ToString().Select(x => int.Parse(x.ToString())).ToList();

                current = digits.Aggregate((a, x) => a * x);
            }
            return loop;
        }
        [TestFixture]
        public class PersistTests
        {
            [Test]
            public void Test1()
            {
                Assert.AreEqual(3, Persistence(39));
            }
            [Test]
            public void Test2()
            {
                Assert.AreEqual(0, Persistence(4));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual(2, Persistence(25));
            }
            [Test]
            public void Test4()
            {
                Assert.AreEqual(4, Persistence(999));
            }
        }

    }
}
