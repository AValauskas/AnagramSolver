using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.Console.Extensions
{
    public static class MyExtension
    {
        public static string CapitalizeFirstLetter(this List<string> anagrams)
        {
            anagrams = anagrams.Select(x => (x.First().ToString().ToUpper() + x.Substring(1))).ToList();
            return String.Join(";", anagrams.ToArray());
        }
      
    }
}
