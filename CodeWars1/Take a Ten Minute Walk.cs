using NUnit.Framework;

namespace CodeWars1
{
    public class TakeATenMinuteWalk
    {
        public static bool IsValidWalk(string[] walk)
        {
            //insert brilliant code here
            int X = 0;
            int Y = 0;
            int blocks = 0;

            foreach (var s in walk)
            {
                blocks++;

                switch (s)
                {
                    case "n":
                        Y++;
                        break;
                    case "s":
                        Y--;
                        break;
                    case "e":
                        X++;
                        break;
                    case "w":
                        X--;
                        break;
                }
            }

            if (blocks == 10 && X == 0 && Y == 0)
                return true;

            return false;
        }

        [Test]
        public void SampleTest()
        {
            Assert.AreEqual(true, IsValidWalk(new string[] { "n", "s", "n", "s", "n", "s", "n", "s", "n", "s" }), "should return true");
            Assert.AreEqual(false, IsValidWalk(new string[] { "w", "e", "w", "e", "w", "e", "w", "e", "w", "e", "w", "e" }), "should return false");
            Assert.AreEqual(false, IsValidWalk(new string[] { "w" }), "should return false");
            Assert.AreEqual(false, IsValidWalk(new string[] { "n", "n", "n", "s", "n", "s", "n", "s", "n", "s" }), "should return false");
        }
    }
}