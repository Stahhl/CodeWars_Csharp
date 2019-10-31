using NUnit.Framework;
using System;
using System.Linq;

namespace CodeWars1
{
    //https://www.codewars.com/kata/did-i-finish-my-sudoku/train/csharp
    class DidIFinishMySudoku
    {
        static int[] values = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        public static string DoneOrNot(int[][] board)
        {
            string result = string.Empty;
            int[,] arr = null;
            try
            {
                arr = To2D(board);

                if (CheckColumns(arr) == false)
                    throw new Exception();
                if (CheckRows(arr) == false)
                    throw new Exception();
                if (CheckRegions(arr) == false)
                    throw new Exception();

                result = "Finished!";
            }
            catch
            {
                result = "Try again!";
            }
            return result;
        }
        static bool CheckColumns(int[,] arr)
        {
            for (int a = 0; a < arr.GetLength(0); a++)
            {
                var list = values.ToList();

                for (int b = 0; b < arr.GetLength(1); b++)
                {
                    if (list.Contains(arr[a, b]) == false)
                        return false;

                    list.Remove(arr[a, b]);
                }
            }

            return true;
        }
        static bool CheckRows(int[,] arr)
        {
            for (int a = 0; a < arr.GetLength(1); a++)
            {
                var list = values.ToList();

                for (int b = 0; b < arr.GetLength(0); b++)
                {
                    if (list.Contains(arr[a, b]) == false)
                        return false;

                    list.Remove(arr[a, b]);
                }
            }

            return true;
        }
        static bool CheckRegions(int[,] arr)
        {
            int X = 3;
            int Y = 3;

            for (int a = 0; a < X; a++)
            {
                for (int b = 0; b < Y; b++)
                {
                    if (CheckRegion(arr, a, b) == false)
                        return false;
                }
            }

            return true;
        }
        static bool CheckRegion(int[,] arr, int x, int y)
        {
            var list = values.ToList();

            int X = x * 3;
            int Y = y * 3;
            int maxX = X + 3;
            int maxY = Y + 3;

            for (int a = X; a < maxX; a++)
            {
                for (int b = Y; b < maxY; b++)
                {
                    if (list.Contains(arr[a, b]) == false)
                        return false;

                    list.Remove(arr[a, b]);
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
        [TestFixture]
        public class Tests
        {
            private static object[] testCase1 = new object[]
            {
                new object[]
                {
                    "Finished!",
                    new int[][]
                    {
                        new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                        new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                        new int[] {1, 9, 8, 3, 4, 2, 5, 6, 7},
                        new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                        new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                        new int[] {7, 1, 3, 9, 2, 4, 8, 5, 6},
                        new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                        new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                        new int[] {3, 4, 5, 2, 8, 6, 1, 7, 9},
                    },
                }
            };
            private static object[] testCase2 = new object[]
            {
                new object[]
                {
                    //fail on first region 5 x 2
                    "Try again!",
                    new int[][]
                    {
                        new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                        new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                        new int[] {1, 9, 5, 3, 4, 2, 8, 6, 7},
                        new int[] {8, 5, 9, 7, 6, 1, 4, 2, 3},
                        new int[] {4, 2, 6, 5, 8, 3, 7, 9, 1},
                        new int[] {7, 1, 3, 9, 2, 4, 5, 8, 6},
                        new int[] {9, 6, 1, 8, 3, 7, 2, 5, 4},
                        new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                        new int[] {3, 4, 8, 2, 5, 6, 1, 7, 9},
                    },
                }
            };
            private static object[] testCase3 = new object[]
            {   
                new object[]
                {
                    "Try again!",
                    new int[][]
                    {
                        new int[] {5, 3, 4, 6, 7, 8, 9, 1, 2},
                        new int[] {6, 7, 2, 1, 9, 5, 3, 4, 8},
                        new int[] {1, 9, 8, 3, 0, 2, 5, 6, 7},
                        new int[] {8, 5, 0, 7, 6, 1, 4, 2, 3},
                        new int[] {4, 2, 6, 8, 5, 3, 7, 9, 1},
                        new int[] {7, 0, 3, 9, 2, 4, 8, 5, 6},
                        new int[] {9, 6, 1, 5, 3, 7, 2, 8, 4},
                        new int[] {2, 8, 7, 4, 1, 9, 6, 3, 5},
                        new int[] {3, 0, 0, 2, 8, 6, 1, 7, 9},
                    },
                },
            };

            [Test, TestCaseSource("testCase1")]
            public void Test1(string expected, int[][] board) => Assert.AreEqual(expected, DoneOrNot(board));

            [Test, TestCaseSource("testCase2")]
            public void Test2(string expected, int[][] board) => Assert.AreEqual(expected, DoneOrNot(board));

            [Test, TestCaseSource("testCase3")]
            public void Test3(string expected, int[][] board) => Assert.AreEqual(expected, DoneOrNot(board));
        }
    }
}
