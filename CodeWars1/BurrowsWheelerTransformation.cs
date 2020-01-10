using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace CodeWars1
{
	//https://www.codewars.com/kata/54ce4c6804fcc440a1000ecb/train/csharp
	public class Kata
	{
		public static Tuple<string, int> Encode(string s)
		{
			var matrix = new string[s.Length];
			string shift = s;

			for (int a = 0; a < matrix.Length; a++)
			{
				shift = ShiftString(shift);
				matrix[a] = shift;
			}

			matrix = matrix.OrderBy(x => x, StringComparer.Ordinal).ToArray();
			var lastRow = CusomWhitespaceSort(matrix);
			var index = Array.IndexOf(matrix, s);

			return new Tuple<string, int>(lastRow, index < 0 ? 0 : index);
		}
		public static string CusomWhitespaceSort(string[] rows)
		{
			var spaceIndexes = new List<int>();

			for (int i = 0; i < rows.Length; i++)
			{
				char a = Char.ToUpper(rows[i][0]);
				char b = Char.ToUpper(rows[i][1]);

				if (rows[i][0] == ' ' && a.CompareTo(b) > 0)
					spaceIndexes.Add(i);
			}
			foreach (var i in spaceIndexes)
			{
				var a = rows[i];
				var b = rows[i + 1];
				rows[i] = b;
				rows[i + 1] = a;
			}

			return String.Join("", rows.Select(x => x.Last().ToString()).ToArray());
		}
		public static string Decode(string s, int key)
		{
			if (string.IsNullOrEmpty(s) == true || key < 0)
				return string.Empty;

			var list = new List<string>();
			var column = new String(s.ToArray());

			foreach (var c in column)
			{
				list.Add(c.ToString());
			}

			for (int i = 0; i < s.Length - 1; i++)
			{
				var last = list.Last();
				list.Insert(0, last);
				list.RemoveAt(list.Count() - 1);
				list = CustomListSort(list);

				for (int a = 0; a < column.Length; a++)
				{
					list[a] = column[a].ToString() + list[a];
				}
			}

			list = CustomListSort(list);

			return list[key];
		}
		public static List<string> CustomListSort(List<string> list)
		{
			//Ignore whitespace
			//Remember where spaces are, delete them, sort list then add them again
			var indexes = new Dictionary<string, List<int>>();

			for (int a = 0; a < list.Count; a++)
			{
				var value = list[a];
				indexes[list[a]] = new List<int>();

				for (int b = 0; b < value.Length; b++)
				{
					if (value[b] == ' ')
					{
						indexes[value].Add(b);
					}

					value.Replace(" ", "");
				}
			}

			list = list.OrderBy(x => x, StringComparer.Ordinal).ToList();

			foreach (var d in indexes)
			{
				foreach (var l in list)
				{
					if(d.Key == l)
					{
						for (int i = 0; i < d.Value.Count; i++)
						{
							l.Insert(i, " ");
						}
					}
				}
			}

			return list;
		}
		public static string ShiftString(string s)
		{
			var last = s.Substring(s.Length - 1, 1);
			var sub = s.Substring(0, s.Length - 1);

			return last + sub;
		}
	}
	[TestFixture]
	public class BurrowsWheelerTransformationTests
	{
		[Test]
		public void EncodingTest1()
		{
			StringAssert.AreEqualIgnoringCase("nnbbraaaa", Kata.Encode("bananabar").Item1);
			Assert.AreEqual(4, Kata.Encode("bananabar").Item2);
		}
		[Test]
		public void EncodingTest2()
		{
			StringAssert.AreEqualIgnoringCase("e emnllbduuHB", Kata.Encode("Humble Bundle").Item1);
			Assert.AreEqual(2, Kata.Encode("Humble Bundle").Item2);
		}
		[Test]
		public void EncodingTest3()
		{
			StringAssert.AreEqualIgnoringCase("ww MYeelllloo", Kata.Encode("Mellow Yellow").Item1);
			Assert.AreEqual(1, Kata.Encode("Mellow Yellow").Item2);
		}
		[Test]
		public void EmptyEncodingTest()
		{
			var kata = Kata.Encode("");
			//StringAssert.AreEqualIgnoringCase("ww MYeelllloo", Kata.Encode("Mellow Yellow").Item1);
			Assert.AreEqual(0, kata.Item2);
		}
		[Test]
		public void DecodingTest1()
		{
			StringAssert.AreEqualIgnoringCase("bananabar", Kata.Decode("nnbbraaaa", 4));
		}
		[Test]
		public void DecodingTest2()
		{
			StringAssert.AreEqualIgnoringCase("Humble Bundle", Kata.Decode("e emnllbduuHB", 2));
		}
		[Test]
		public void DecodingTest3()
		{
			StringAssert.AreEqualIgnoringCase("Mellow Yellow", Kata.Decode("ww MYeelllloo", 1));
		}
		[Test]
		public void DecodingTest4()
		{
			StringAssert.AreEqualIgnoringCase(new String('x', 20), Kata.Decode(new String('x', 20), 0));
		}
		[Test]
		public void DecodingTest5()
		{
			StringAssert.AreEqualIgnoringCase("CODE", Kata.Decode("EODC", 0));
		}
	}
}
