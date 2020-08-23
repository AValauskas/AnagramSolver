using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.Console.Extensions
{
    public static class MyExtension
    {
        public static string CapitalizeFirstLetter(this IEnumerable<string> anagrams)
        {
            anagrams = anagrams.Select(x => (x.First().ToString().ToUpper() + x.Substring(1)));
            return String.Join(";", anagrams);
        }
      
    }
}
