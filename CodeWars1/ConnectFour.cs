using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace CodeWars1
{
    //https://www.codewars.com/kata/56882731514ec3ec3d000009/train/csharp
    class ConnectFour
    {
        enum Color { Null, Red, Yellow, Foo };
        public static string WhoIsWinner(List<string> piecesPositionList)
        {
            char[] letters = "ABCDEFG".ToCharArray();
            Color[,] board = new Color[8, 7];
            string result = string.Empty;

            foreach (var str in piecesPositionList)
            {
                var split = str.Split("_");
                int x = Array.IndexOf(letters, char.Parse(split[0]));
                int y = 0;

                for (int i = 0; i < board.GetLength(1); i++)
                {
                    if (board[x, i] == Color.Null)
                    {
                        y = i;
                        break;
                    }
                }

                board[x, y] = (Color)Enum.Parse(typeof(Color), split[1], true);

                result = CheckWin(board);

                if(result != "Draw")
                    break;
            }

            return result;
        }
        static string CheckWin(Color[,] board)
        {
            int X = board.GetLength(0);
            int Y = board.GetLength(1);

            for (int x = 0; x < X; x++)
            {
                for (int y = 0; y < Y; y++)
                {
                    var color = board[x, y];

                    if (color == Color.Null || color == Color.Foo)
                        continue;

                    int max = 3;
                    //NORTH
                    if(y <= max)
                    {
                        if (board[x, y + 1] == color &&
                            board[x, y + 2] == color &&
                            board[x, y + 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //NORTH EAST
                    if(y <= max && x <= max)
                    {
                        if (board[x + 1, y + 1] == color &&
                            board[x + 2, y + 2] == color &&
                            board[x + 3, y + 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //EAST
                    if (x <= max)
                    {
                        if (board[x + 1, y] == color &&
                            board[x + 2, y] == color &&
                            board[x + 3, y] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //SOUTH EAST
                    if (y >= max && x <= max)
                    {
                        if (board[x + 1, y - 1] == color &&
                            board[x + 2, y - 2] == color &&
                            board[x + 3, y - 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //SOUTH
                    if (y >= max)
                    {
                        if (board[x, y - 1] == color &&
                            board[x, y - 2] == color &&
                            board[x, y - 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //SOUTH WEST
                    if (y >= max && x >= max)
                    {
                        if (board[x - 1, y - 1] == color &&
                            board[x - 2, y - 2] == color &&
                            board[x - 3, y - 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //WEST
                    if (x >= max)
                    {
                        if (board[x - 1, y] == color &&
                            board[x - 2, y] == color &&
                            board[x - 3, y] == color)
                        {
                            return color.ToString();
                        }
                    }
                    //NORTH WEST
                    if (y <= max && x >= max)
                    {
                        if (board[x - 1, y + 1] == color &&
                            board[x - 2, y + 2] == color &&
                            board[x - 3, y + 3] == color)
                        {
                            return color.ToString();
                        }
                    }
                }
            }

            return "Draw";
        }
    }
    [TestFixture]
    public class ConnectFourTest
    {
        [Test]
        public void ColumnATest()
        {
            List<string> myList = new List<string>()
            {
                "A_Red",
                "A_Yellow",
                "A_Red",
                "A_Yellow",
                "A_Red",
                "A_Yellow",
            };
            StringAssert.AreEqualIgnoringCase("Draw", ConnectFour.WhoIsWinner(myList), "it should return Draw");
        }
        [Test]
        public void ColumnGTest()
        {
            List<string> myList = new List<string>()
            {
                "G_Red",
                "G_Yellow",
                "G_Red",
                "G_Yellow",
                "G_Red",
                "G_Yellow",
            };
            StringAssert.AreEqualIgnoringCase("Draw", ConnectFour.WhoIsWinner(myList), "it should return Draw");
        }
        [Test]
        public void Row1TEst()
        {
            List<string> myList = new List<string>()
            {
                "A_Red",
                "B_Yellow",
                "C_Red",
                "D_Yellow",
                "E_Red",
                "F_Yellow",
                "G_Red",
            };
            StringAssert.AreEqualIgnoringCase("Draw", ConnectFour.WhoIsWinner(myList), "it should return Draw");
        }
        [Test]
        public void FullBoardTest()
        {
            List<string> myList = new List<string>()
            {
                "A_Foo",
                "A_Foo",
                "A_Foo",
                "A_Foo",
                "A_Foo",
                "A_Foo",
                
                "B_Foo",
                "B_Foo",
                "B_Foo",
                "B_Foo",
                "B_Foo",
                "B_Foo",
                
                "C_Foo",
                "C_Foo",
                "C_Foo",
                "C_Foo",
                "C_Foo",
                "C_Foo",
                
                "D_Foo",
                "D_Foo",
                "D_Foo",
                "D_Foo",
                "D_Foo",
                "D_Foo",
                
                "E_Foo",
                "E_Foo",
                "E_Foo",
                "E_Foo",
                "E_Foo",
                "E_Foo",
                
                "F_Foo",
                "F_Foo",
                "F_Foo",
                "F_Foo",
                "F_Foo",
                "F_Foo",
                
                "G_Foo",
                "G_Foo",
                "G_Foo",
                "G_Foo",
                "G_Foo",
                "G_Foo",
            };
            StringAssert.AreEqualIgnoringCase("Draw", ConnectFour.WhoIsWinner(myList), "it should return Draw");
        }
        [Test]
        public void FirstTest()
        {
            List<string> myList = new List<string>()
            {
                "A_Red",
                "B_Yellow",
                "A_Red",
                "B_Yellow",
                "A_Red",
                "B_Yellow",
                "G_Red",
                "B_Yellow"
            };
            StringAssert.AreEqualIgnoringCase("Yellow", ConnectFour.WhoIsWinner(myList), "it should return Yellow");
        }

        [Test]
        public void SecondTest()
        {
            List<string> myList = new List<string>()
            {
                "C_Yellow",
                "E_Red",
                "G_Yellow",
                "B_Red",
                "D_Yellow",
                "B_Red",
                "B_Yellow",
                "G_Red",
                "C_Yellow",
                "C_Red",
                "D_Yellow",
                "F_Red",
                "E_Yellow",
                "A_Red",
                "A_Yellow",
                "G_Red",
                "A_Yellow",
                "F_Red",
                "F_Yellow",
                "D_Red",
                "B_Yellow",
                "E_Red",
                "D_Yellow",
                "A_Red",
                "G_Yellow",
                "D_Red",
                "D_Yellow",
                "C_Red"
            };
            StringAssert.AreEqualIgnoringCase("Yellow", ConnectFour.WhoIsWinner(myList));
        }

        [Test]
        public void ThirdTest()
        {
            List<string> myList = new List<string>()
            {
                "A_Yellow", //
                "B_Red", //
                "B_Yellow", //
                "C_Red", //
                "G_Yellow", //
                "C_Red", //
                "C_Yellow", //
                "D_Red", //
                "G_Yellow",//
                "D_Red", //
                "G_Yellow", //
                "D_Red", //
                "F_Yellow", //
                "E_Red", //win
                "D_Yellow"
            };
            StringAssert.AreEqualIgnoringCase("Red", ConnectFour.WhoIsWinner(myList), "it should return Red");
        }
    }
}
