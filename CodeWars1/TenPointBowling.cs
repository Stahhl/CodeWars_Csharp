using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars1
{
    //https://www.codewars.com/kata/5531abe4855bcc8d1f00004c/train/csharp
    //TODO tappar bort 5 poäng i list[7]
    public class TenPointBowling
    {
        public static int BowlingScore(string frames)
        {
            var frameList = frames.Split(" ").Select(x => x.Trim().ToUpper()).ToList();

            var frameCells = new List<int[,,]>();
            var frameScores = new List<int>();
            var stepScores = new List<int>();

            for (int i = 0; i < frameList.Count; i++)
            {
                int[,,] cell = getFrameCell(frameList[i]);

                frameCells.Add(cell);
            }
            for (int i = 0; i < frameList.Count; i++)
            {
                int bonus = i < 9 ? getFrameBonus(frameList, frameCells, i) : 0;

                frameScores.Add(getCellValue(frameCells[i]) + bonus);
                stepScores.Add(frameScores.Sum());
            }

            int result = frameScores.Sum();

            return result;
        }
        private static int getFrameBonus(List<string> sList, List<int[,,]> cList, int i)
        {
            var sValue = sList[i];
            int result = 0;
            int max = 9;

            var s1 =  i + 1 >= max ? null : sList[i + 1];
            var s2 = i + 2 >= max ? null : sList[i + 2];

            var c1 =  i + 1 >= max ? null :  cList[i + 1];
            var c2 = i + 2 >= max ? null : cList[i + 2];

            if(i == 7)
            {
                s2 = sList[9][0].ToString();
                c2 = new int[cList[9].GetLength(0), 0, 0];
            }
            if(i == 8)
            {
                s1 = sList[9][0].ToString();
                c1 = new int[cList[9].GetLength(0), 0, 0];

                s2 = sList[9][1].ToString();
                c2 = new int[cList[9].GetLength(1), 0, 0];

                if (sValue.Contains("X") && s2.Contains("/"))
                    result += c2.GetLength(0);
                if (sValue.Contains("X") && int.TryParse(s1 + s2, out int x))
                    c1 = new int[c1.GetLength(0), c2.GetLength(0),0];
            }


            if(sValue.Contains("X"))
            {
                result += getCellValue(c1);

                if (s1.Contains("X"))
                    result += s2.Contains("X") ? getCellValue(c2) : c2.GetLength(0);
            }
            if(sValue.Contains("/"))
            {
                result += c1.GetLength(0);
            }

            return result;
        }
        private static int[,,] getFrameCell(string frame)
        {
            var cell = new int[3];

            for (int i = 0; i < frame.Length; i++)
            {
                char c = frame[i];

                if (c == 'X')
                    cell[i] = 10;
                else if (c == '/')
                    cell[i] = 10 - cell[i - 1];
                else
                    cell[i] = (int)char.GetNumericValue(c);
            }

            return new int[cell[0], cell[1], cell[2]];
        }
        private static int getCellValue(int[,,] cell)
        {
            return cell.GetLength(0) + cell.GetLength(1) + cell.GetLength(2);
        }
    }
    [TestFixture]
    public class TenPointBowlingTest
    {
        [Test]
        public void Test01()
        {
            Assert.AreEqual(20, TenPointBowlingBestPractice.BowlingScore("11 11 11 11 11 11 11 11 11 11"));
        }
        [Test]
        public void Test02()
        {
            Assert.AreEqual(300, TenPointBowlingBestPractice.BowlingScore("X X X X X X X X X XXX"));
        }
        [Test]
        public void Test03()
        {
            Assert.AreEqual(145, TenPointBowlingBestPractice.BowlingScore("9/ 44 X 32 7/ 5/ 71 12 X XXX"));
        }
        [Test]
        public void Test04()
        {
            Assert.AreEqual(30, TenPointBowlingBestPractice.BowlingScore("X 11 11 11 11 11 11 11 11 11"));
        }
        [Test]
        public void Test05()
        {
            Assert.AreEqual(281, TenPointBowlingBestPractice.BowlingScore("X X X X X X X X X X51"));
        }
        [Test]
        public void Test06()
        {
            Assert.AreEqual(150, TenPointBowlingBestPractice.BowlingScore("5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/ 5/5"));
        }
        [Test]
        public void Test07()
        {
            Assert.AreEqual(133, TenPointBowlingBestPractice.BowlingScore("X 5/ X 72 90 X 3/ 54 35 22"));
        }
        [Test]
        public void Test08()
        {
            Assert.AreEqual(88, TenPointBowlingBestPractice.BowlingScore("X 00 X 5/ 00 42 6/ X 10 X00"));
        }
        [Test]
        public void Test09()
        {
            Assert.AreEqual(90, TenPointBowlingBestPractice.BowlingScore("X X X X 00 00 00 00 00 00"));
        }
        [Test]
        public void Test10()
        {
            Assert.AreEqual(40, TenPointBowlingBestPractice.BowlingScore("5/ X 00 X 00 00 00 00 00 00"));
        }
        [Test]
        public void Test11()
        {
            Assert.AreEqual(30, TenPointBowlingBestPractice.BowlingScore("00 00 00 00 00 00 00 00 00 XXX"));
        }
        [Test]
        public void Test12()
        {
            Assert.AreEqual(85, TenPointBowlingBestPractice.BowlingScore("0/ 10 21 32 43 54 60 71 9/ X32"));
        }
        [Test]
        public void Test13()
        {
            Assert.AreEqual(40, TenPointBowlingBestPractice.BowlingScore("x 00 00 00 00 00 00 00 x 0/0"));
        }
        [Test]
        public void Test14()
        {
            Assert.AreEqual(95, TenPointBowlingBestPractice.BowlingScore("70 07 X X 01 10 X X 04 2/0"));
        }
        [Test]
        public void Test15()
        {
            Assert.AreEqual(265, TenPointBowlingBestPractice.BowlingScore("X X X X X X X X 5/ X5/"));
        }
        [Test]
        public void Test16()
        {
            Assert.AreEqual(70, TenPointBowlingBestPractice.BowlingScore("00 00 00 00 00 00 00 5/ X XX0"));
        }
        [Test]
        public void Test17()
        {
            Assert.AreEqual(150, TenPointBowlingBestPractice.BowlingScore("x x x x x x 00 00 00 00"));
        }
        [Test]
        public void Test18()
        {
            Assert.AreEqual(170, TenPointBowlingBestPractice.BowlingScore("x x x x x x 00 00 00 5/X"));
        }
        [Test]
        public void Test19()
        {
            Assert.AreEqual(50, TenPointBowlingBestPractice.BowlingScore("00 00 00 00 00 00 00 X X 44"));
        }
    }
}

public static class TenPointBowlingBestPractice
{
    public static int AToI(char c)
    {
        return c == 'X' ? 10 : int.Parse(c.ToString());
    }
    public static int BowlingScore(string frames)
    {
        int frameIdx = 0;
        int score = 0;
        string rolls = frames.ToUpper().Replace(" ", "");
        int totalRolls = rolls.Length;
        for (int t = 0; t < totalRolls && frameIdx < 20; ++t)
        {
            if (rolls[t] == 'X')
            {
                score += 10;
                score += AToI(rolls[t + 1]);
                if (rolls[t + 2] == '/')
                {
                    score += 10 - AToI(rolls[t + 1]);
                }
                else
                {
                    score += AToI(rolls[t + 2]);
                }
                frameIdx += 2;
            }
            else if (rolls[t] == '/')
            {
                score += 10 - AToI(rolls[t - 1]);
                score += AToI(rolls[t + 1]);
                frameIdx += 1;
            }
            else
            {
                score += AToI(rolls[t]);
                frameIdx += 1;
            }
        }


        return score;
    }
}