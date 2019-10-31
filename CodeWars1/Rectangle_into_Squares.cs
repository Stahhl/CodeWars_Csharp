using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace CodeWars1
{
    class Rectangle_into_Squares
    {
        //https://www.codewars.com/kata/rectangle-into-squares/train/csharp
        public static List<int> sqInRect(int lng, int wdth)
        {
            if (lng == wdth) 
                return null;

            var squares = new List<int>();

            while (lng > 0 && wdth > 0)
            {
                if (lng < wdth)
                {
                    squares.Add(lng);
                    wdth -= lng;
                }
                else
                {
                    squares.Add(wdth);
                    lng -= wdth;
                }
            }

            return squares;
        }
        [TestFixture]
        public class SqInRectTests
        {
            [Test]
            public void Test1()
            {
                int[] r = new int[] { 3, 2, 1, 1 };
                Assert.AreEqual(r, sqInRect(5, 3));
            }
            [Test]
            public void Test3()
            {
                Assert.AreEqual(null, sqInRect(5, 5));
            }
        }
    }
}
