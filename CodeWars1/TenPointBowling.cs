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
            var frameList = frames.Split(" ").Select(x => x.Trim()).ToList();

            var frameCells = new List<int[,,]>();
            var frameScores = new List<int>();

            for (int i = 0; i < frameList.Count; i++)
            {
                int[,,] cell = getFrameCell(frameList[i]);

                frameCells.Add(cell);
            }
            for (int i = 0; i < frameList.Count; i++)
            {
                int bonus = i < 9 ? getFrameBonus(frameList, frameCells, i) : 0;

                frameScores.Add(getCellValue(frameCells[i]) + bonus);
            }

            int result = frameScores.Sum();

            return result;
        }
        private static int getFrameBonus(List<string> sList, List<int[,,]> cList, int i)
        {
            var sValue = sList[i];

            var max = 9;

            var s1 =  i + 1 >= max ? sList[9][0].ToString() : sList[i + 1];
            var s2 = i + 2 >= max ? sList[9][1].ToString() : sList[i + 2];

            var c1 =  i + 1 >= max ? new int[cList[9].GetLength(0), 0, 0] :  cList[i + 1];
            var c2 = i + 2 >= max ? new int[cList[9].GetLength(1), 0, 0] : cList[i + 2];


            int result = 0;

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

            if(i + 2 >= max)
            {

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
        public void Test1()
        {
            Console.WriteLine("Maybe this bowler should put bumpers on...\n ");
            Assert.AreEqual(20, TenPointBowling.BowlingScore("11 11 11 11 11 11 11 11 11 11"));
        }
        [Test]
        public void Test2()
        {
            Console.WriteLine("Woah! Perfect game!");
            Assert.AreEqual(300, TenPointBowling.BowlingScore("X X X X X X X X X XXX"));
        }
        [Test]
        public void Test3()
        {
            Assert.AreEqual(145, TenPointBowling.BowlingScore("9/ 44 X 32 7/ 5/ 71 12 X XXX"));
        }
        [Test]
        public void Test4()
        {
            Assert.AreEqual(30, TenPointBowling.BowlingScore("X 11 11 11 11 11 11 11 11 11"));
        }
        [Test]
        public void Test5()
        {
            Console.WriteLine("Woah! Perfect game!");
            Assert.AreEqual(281, TenPointBowling.BowlingScore("X X X X X X X X X X51"));
        }
    }
}


//          for (int i = 0; i<fArray.Length; i++)
//            {
//                string f = fArray[i].ToUpper().Trim();
//int strikeMulti = 1;

//fScore = 0;

//                if(strikes > 0)
//                {
//                    strikeMulti = 3;

//                    strikes--;
//                }
//                if (f == "X")
//                {
//                    fScore += 10;

//                    strikes += 2;
//                }
//                if (f.Contains('/') == true)
//                {
//                    f = "/";
//                    fScore += 10;
//                    spares++;
//                }
//                if(f == "XXX")
//                {
//                    fScore += 30;
//                    strikeMulti = 1;
//                    strikes = 0;
//                }
//                if (int.TryParse(f, out int digit) == true)
//                {
//                    foreach (var c in f)
//                    {
//                        int spareMulti = 1;

//                        if (spares > 0)
//                        {
//                            spareMulti = 2;
//                            spares--;
//                        }

//                        fScore += (int) Char.GetNumericValue(c) * spareMulti;
//                    }
//                }

//                tScore += fScore* strikeMulti;
//            }



//int tScore = 0;

//string[] fArray = frames.Split(" ");

//var frameScore = new List<int[,]>();

//var totalScore = new List<int>();
//var frameMulti = new List<string>();

//            foreach (var f in fArray)
//            {
//                int[,] s = new int[0, 0];
//string m = "";

//                if (f.Contains("X"))
//                {
//                    s = new int[10, 0];
//                    m = "X";

//                    if (f.Length > 1)
//                    {
//                        tScore += 30;
//                        //foreach (var x in f)
//                        //{
//                        //    //m = "";

//                        //    //frameScore.Add(s);
//                        //    //frameMulti.Add(m);
//                        //    tScore += 10;
//                        //}
//                        //continue;
//                    }
//                }
//                if (f.Contains("/"))
//                {
//                    m = "/";

//                    s = new int[10, 0];
//                }
//                else if (int.TryParse(f, out int digit))
//                {
//                    var d1 = (int)Char.GetNumericValue(f[0]);
//var d2 = (int)Char.GetNumericValue(f[1]);

//s = new int[d1, d2];
//                }

//                frameScore.Add(s);
//                frameMulti.Add(m);
//            }

//            int max = frameScore.Count;

//            for (int i = 0; i<max; i++)
//            {
//                int fScore = cellValue(frameScore[i]);

//                if (i + 1 < max && frameMulti[i] == "X")
//                {
//                    fScore += cellValue(frameScore[i + 1]);
//                }
//                if (i + 2 < max && frameMulti[i + 1] == "X")
//                {
//                    fScore += cellValue(frameScore[i + 2]);
//                }
//                if (i + 1 < max && frameMulti[i] == "/")
//                {

//                }

//                tScore += fScore;
//                totalScore.Add(fScore);
//            }

//            return tScore;