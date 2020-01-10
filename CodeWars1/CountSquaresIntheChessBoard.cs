using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars1
{
    //https://www.codewars.com/kata/5bc6f9110ca59325c1000254/train/csharp
    class ChessBoard
    {
        //Best practice
        public static Dictionary<int, int> Count2(int[][] a, int[,] x = null)
        {
            var n = a.Length;
            var results = new Dictionary<int, int>();
            var index = 2;

            while (index - 1 < n)
            {
                var sub = 0;
                var edge = n - 1;
                var bottom = index - 2;
                for (var i = edge - 1; i > bottom - 1; i = i - 1)
                {
                    if (a[i][edge] == 0)
                        a[i + 1][edge] = 0;
                    if (a[edge][i] == 0)
                        a[edge][i + 1] = 0;
                }

                for (var i = edge - 1; i > bottom - 1; i = i - 1)
                    for (var j = edge - 1; j > bottom - 1; j = j - 1)
                    {
                        if (a[i][j] == 0)
                        {
                            a[i + 1][j] = 0;
                            a[i][j + 1] = 0;
                            a[i + 1][j + 1] = 0;
                        }
                    }

                for (var i = edge; i > bottom; i = i - 1)
                    for (var j = edge; j > bottom; j = j - 1)
                        if (a[i][j] == 1)
                            sub = sub + 1;

                if (sub == 0)
                    break;
                else
                    results.Add(index, sub);
                index = index + 1;
            }

            return results;
        }
        //Mine too slow :(
        public static Dictionary<int, int> Count(int[][] b, int[,] x = null)
        {
            var arr = x == null ? To2D(b) : x;

            var max = arr.GetLength(0);
            var min = 2; //min 2x2 square

            var regions = new Dictionary<int, int>();

            for (int i = min; i <= max; i++)
            {
                var amount = CheckRegions(arr, max, i);

                if(amount > 0)
                    regions[i] = CheckRegions(arr, max, i);
            }

            return regions;
        }
        static int CheckRegions(int[,] arr, int max, int i)
        {
            int amount = 0;
            int X = max;
            int Y = max;

            for (int a = 0; a < X; a++)
            {
                for (int b = 0; b < Y; b++)
                {
                    if (a > max - i || b > max - i)
                        break;

                    if (CheckRegion(arr, i, a, b) == true)
                        amount++;
                }
            }

            return amount;
        }
        static bool CheckRegion(int[,] arr, int i, int x, int y)
        {
            int maxX = x + i;
            int maxY = y + i;

            for (int a = x; a < maxX; a++)
            {
                for (int b = y; b < maxY; b++)
                {
                    if (arr[a,b] == 0)
                        return false;
                }
            }

            return true;
        }
        static T[,] To2D<T>(T[][] source)
        {
            int FirstDim = source.Length;
            int SecondDim = source.GroupBy(row => row.Length).Single().Key; // throws InvalidOperationException if source is not rectangular

            var result = new T[FirstDim, SecondDim];
            for (int i = 0; i < FirstDim; ++i)
                for (int j = 0; j < SecondDim; ++j)
                    result[i, j] = source[i][j];

            return result;
        }
    }

    public class ChessBoardTests
    {
        [Test]
        public void Test_B()
        {
            var chessBoard = new int[][]
            {
                new int[] {1,1},
                new int[] {1,1}
            };

            var one = new Dictionary<int, int> { [2] = 1 };
            var two = ChessBoard.Count(chessBoard);

            Assert.AreEqual(one, two);
        }
        [Test]
        public void Test_A()
        {
            var chessBoard = new int[][]
            {
                new int[] {1,1},
                new int[] {1,1}
            };

            var one = new Dictionary<int, int> { [2] = 1 };
            var three = ChessBoard.Count2(chessBoard);

            Assert.AreEqual(one, three);
        }
        [Test]
        public void Test2()
        {
            var chessBoard = new int[][]
            {
                new int[] {0,1},
                new int[] {1,1}
            };
            CollectionAssert.AreEquivalent(new Dictionary<int, int>(), ChessBoard.Count(chessBoard));
        }
        [Test]
        public void Test3()
        {
            var chessBoard = new int[][]
            {
                new int[] {0,1},
                new int[] {1,1}
            };
            CollectionAssert.AreEquivalent(new Dictionary<int, int>(), ChessBoard.Count(chessBoard));
        }
        [Test]
        public void Test4()
        {
            var chessBoard = new int[][]
            {
            new int[] {1,1,1},
            new int[] {1,0,1},
            new int[] {1,1,1}
            };
            CollectionAssert.AreEquivalent(new Dictionary<int, int>(), ChessBoard.Count(chessBoard));
        }
        [Test]
        public void Test5()
        {
            var chessBoard = new int[][]
            {
            new int[] {0,1,1,1,1},
            new int[] {1,1,1,1,1},
            new int[] {1,1,1,1,1},
            new int[] {0,1,1,0,1},
            new int[] {1,1,1,1,1}
            };
            CollectionAssert.AreEquivalent(new Dictionary<int, int> { [3] = 2, [2] = 9 }, ChessBoard.Count(chessBoard));
        }
        [Test]
        public void Test6()
        {
            var chessBoard = new int[219, 219];
            var rand = new Random();

            for (int x = 0; x < chessBoard.GetLength(0); x++)
            {
                for (int y = 0; y < chessBoard.GetLength(1); y++)
                {
                    chessBoard[x, y] = rand.Next(2);
                }
            }

            CollectionAssert.AreNotEquivalent(new Dictionary<int, int>(), ChessBoard.Count(null, chessBoard));
        }
    }
}
