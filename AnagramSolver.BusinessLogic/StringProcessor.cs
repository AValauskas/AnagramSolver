using System;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class StringProcessor
    {
        public static bool IsMatch(string key, string word)
        {
            var myChar = word.ToCharArray();
            for (int i = 0; i < key.Length; i++)
            {
                if (myChar.Contains(key[i]))
                    myChar = RemoveChar(myChar, key[i]);
            }
            if (myChar.Length == 0)
                return true;

            return false;
        }

        public static string RemoveSomeLettersString(string key, string word)
        {
            var myChar = word.ToCharArray();
            var sb = new StringBuilder();
            for (int i = 0; i < key.Length; i++)
            {
                if (!myChar.Contains(key[i]))
                {
                    sb.Append(key[i]);
                }
                else
                {
                    myChar = RemoveChar(myChar, key[i]);
                }
            }
            return sb.ToString();
        }

        public static char[] RemoveChar(char[] myChar, char letter)
        {
            string str = new string(myChar);
            int index = str.IndexOf(letter);
            str = str.Remove(index, 1);
            myChar = str.ToCharArray();
            return myChar;
        }
    }
}
