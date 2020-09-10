using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace RedBlackTree
{
    static class SimpleParser
    {
        public static string RemoveSpecialCharactersExceptSpace(this string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if (Char.IsLetter(c))
                    sb.Append(c);
                else
                    sb.Append(' ');
            }
            return sb.ToString();
        }

        public static List<string> Words(this StreamReader sr)
        {
            StreamReader _sr = sr ?? throw new Exception("Can't parse. StreamReader == null");

            List<string> words = new List<string>();

            string line;
            char[] separator = { ' ' };

            while ((line = _sr.ReadLine()) != null)
            {
                line = RemoveSpecialCharactersExceptSpace(line);
                line = line.ToLowerInvariant();
                string[] split = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                foreach (string w in split)
                {
                    if (!words.Contains(w))
                        words.Add(w);
                }
            }
            return words;
        }
    }
}
