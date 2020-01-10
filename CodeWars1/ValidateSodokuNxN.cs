using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeWars1
{
    //https://www.codewars.com/kata/540afbe2dc9f615d5e000425/train/csharp
    public class Sudoku
    {
        public Sudoku(int[][] sudokuData)
        {
            board = sudokuData;
        }

        private static int[][] board;
        private static int[,] arr;
        private static int[] values;
        private static int sqrtRegions;

        public bool IsValid()
        {
            try
            {
                arr = To2D(board);

                int max = arr.Cast<int>().Max();
                int min = arr.Cast<int>().Min();

                double sqrt = Math.Sqrt(max);

                sqrtRegions = (int)sqrt;

                var list = new List<int>();

                for (int i = min; i < max + 1; i++)
                {
                    list.Add(i);
                }

                values = list.ToArray();

                if (min <= 0)
                    throw new Exception();
                if (sqrt != Math.Round(sqrt, 0))
                    throw new Exception();
                if (CheckColumns(arr) == false)
                    throw new Exception();
                if (CheckRows(arr) == false)
                    throw new Exception();
                if (CheckRegions(arr) == false)
                    throw new Exception();

                return true;
            }
            catch
            {
                return false;
            }
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
            int X = sqrtRegions;
            int Y = sqrtRegions;

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

            int X = x * sqrtRegions;
            int Y = y * sqrtRegions;
            int maxX = X + sqrtRegions;
            int maxY = Y + sqrtRegions;

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
    }
    [TestFixture]
    public class SudokuTests
    {

        [Test]
        public void Test1()
        {
            var goodSudoku1 = new Sudoku(
              new int[][] {
                new int[] {7,8,4, 1,5,9, 3,2,6},
                new int[] {5,3,9, 6,7,2, 8,4,1},
                new int[] {6,1,2, 4,3,8, 7,5,9},

                new int[] {9,2,8, 7,1,5, 4,6,3},
                new int[] {3,5,7, 8,4,6, 1,9,2},
                new int[] {4,6,1, 9,2,3, 5,8,7},

                new int[] {8,7,6, 3,9,4, 2,1,5},
                new int[] {2,4,3, 5,6,1, 9,7,8},
                new int[] {1,9,5, 2,8,7, 6,3,4}
              });
            Assert.IsTrue(goodSudoku1.IsValid());
        }

        [Test]
        public void Test2()
        {
            var goodSudoku2 = new Sudoku(
              new int[][] {
                new int[] {1,4, 2,3},
                new int[] {3,2, 4,1},

                new int[] {4,1, 3,2},
                new int[] {2,3, 1,4}
            });
            Assert.IsTrue(goodSudoku2.IsValid());
        }

        [Test]
        public void Test3()
        {
            var badSudoku1 = new Sudoku(
              new int[][] {
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},

                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9},
                new int[] {1,2,3, 4,5,6, 7,8,9}
              });
            Assert.IsFalse(badSudoku1.IsValid());
        }

        [Test]
        public void Test4()
        {
            var badSudoku2 = new Sudoku(
              new int[][] {
                new int[] {1,2,3,4,5},
                new int[] {1,2,3,4},

                new int[] {1,2,3,4},
                new int[] {1}
            });
            Assert.IsFalse(badSudoku2.IsValid());
        }
    }
}
