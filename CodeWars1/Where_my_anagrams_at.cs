using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars1
{
    class Where_my_anagrams_at
    {
        public static List<string> Anagrams(string word, List<string> words)
        {
            List<string> result = new List<string>();

            var a = new String(word.OrderBy(x => x).ToArray());

            foreach (var item in words)
            {
                var b = new String(item.OrderBy(y => y).ToArray());

                if (a == b)
                    result.Add(item);
            }

            return result;
        }
        [TestFixture]
        public class SolutionTest
        {
            [Test]
            public void SampleTest()
            {
                Assert.AreEqual(new List<string> { "a" }, Anagrams("a", new List<string> { "a", "b", "c", "d" }));
                Assert.AreEqual(new List<string> { "carer", "arcre", "carre" }, Anagrams("racer", new List<string> { "carer", "arcre", "carre", "racrs", "racers", "arceer", "raccer", "carrer", "cerarr" }));
            }
        }
    }
}
