using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeWars1
{
    //https://www.codewars.com/kata/58c5577d61aefcf3ff000081/train/csharp
    //https://www.boxentriq.com/code-breaking/rail-fence-cipher
    public class RailFenceCipher
    {
        public static string Encode(string s, int n)
        {
            try
            {
                return encode(s, n);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public static string Decode(string s, int n)
        {
            try
            {
                return decode(s, n);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        private static string encode(string s, int n)
        {
            if (s == string.Empty)
                return s;

            var coords = zigZag(s, n);
            var result = string.Empty;
            var map = new string[s.Length, n];

            int loop = 0;

            for (int x = 0; x < s.Length; x++)
            {
                var hits = coords.Where(c => c.GetLength(0) == x).ToArray();

                if(hits.Length > 0)
                {
                    map[hits[0].GetLength(0), hits[0].GetLength(1)] += s[loop];
                    loop++;
                }
            }
            for (int y = 0; y < n; y++)
            {
                string str = string.Empty;
                for (int x = 0; x < s.Length; x++)
                {
                    if (string.IsNullOrEmpty(map[x, y]) == false)
                        str += map[x, y];
                }
                result = str += result;
            }

            return result;
        }
        private static string decode(string s, int n)
        {
            if (s == string.Empty)
                return s;

            var coords = zigZag(s, n);
            var result = string.Empty;
            var map = new string[s.Length, n];

            int loop = 0;

            for (int y = n - 1; y > -1; y--)
            {
                for (int x = 0; x < s.Length; x++)
                {
                    var hits = coords.Where(c => c.GetLength(0) == x && c.GetLength(1) == y).ToArray();

                    if(hits.Length > 0)
                    {
                        map[x, y] += s[loop];
                        loop++;
                    }
                }
            }

            for (int x = 0; x < s.Length; x++)
            {
                for (int y = 0; y < n; y++)
                {
                    if (string.IsNullOrEmpty(map[x, y]) == false)
                        result += map[x, y];
                }
            }

            return result;
        }
        private static List<int[,]> zigZag(string s, int n)
        {
            var list = new List<int[,]>();

            bool upDown = false;

            for (int x = 0; x < s.Length; x += 0)
            {
                if (upDown == false)
                {
                    for (int y = 0; y < n; y++)
                    {
                        if (x >= s.Length)
                            break;

                        list.Add(new int[x, n - y - 1]);
                        x++;
                    }

                    upDown = true;
                }
                else if (upDown == true)
                {
                    for (int y = 0; y < n - 2; y++)
                    {
                        if (x >= s.Length)
                            break;

                        list.Add(new int[x, y + 1]);
                        x++;
                    }

                    upDown = false;
                }
            }

            return list;
        }
    }
    public class RailFenceCipherTests
    {
        [Test]
        public void EncodeSampleTests()
        {
            string[][] encodes =
            {
                new[] { "0123456789", "0481357926" },  // 3 rails
                new[] { "WEAREDISCOVEREDFLEEATONCE", "WECRLTEERDSOEEFEAOCAIVDEN" },  // 3 rails
                new[] { "Hello, World!", "Hoo!el,Wrdl l" },    // 3 rails
                new[] { "Hello, World!", "H !e,Wdloollr" },    // 4 rails
                new[] { "", "" }                               // 3 rails (even if...)
            };
            int[] rails = { 3, 3, 3, 4, 3 };
            for (int i = 0; i < encodes.Length; i++)
            {
                Assert.AreEqual(encodes[i][1], RailFenceCipher.Encode(encodes[i][0], rails[i]));
            }
        }

        [Test]
        public void DecodeSampleTests()
        {
            string[][] decodes =
            {
                new[] { "0481357926", "0123456789" },
                new[] { "H !e,Wdloollr", "Hello, World!" },    // 4 rails
                new[] { "WECRLTEERDSOEEFEAOCAIVDEN", "WEAREDISCOVEREDFLEEATONCE" },    // 3 rails
                new[] { "", "" }                               // 3 rails (even if...)
            };
            int[] rails = { 3, 4, 3, 3 };
            for (int i = 0; i < decodes.Length; i++)
            {
                Assert.AreEqual(decodes[i][1], RailFenceCipher.Decode(decodes[i][0], rails[i]));
            }
        }
    }
}
