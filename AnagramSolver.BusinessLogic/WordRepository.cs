using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic
{
    public class WordRepository : IWordRepository
    {
        public Dictionary<string, List<Anagram>> ReadFile()
        {
            Dictionary<string, List<Anagram>> anagrams = new Dictionary<string, List<Anagram>>();
            string path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Contracts/Files/zodynas.txt");
            string lastWord="";
            string sortedWord = "";

            using (FileStream fs = File.Open(path, FileMode.Open))            
            using (StreamReader sr = new StreamReader(fs))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    string[] row = s.Split("\t");                   
                    sortedWord = String.Concat(row[0].OrderBy(c => c));
                    
                    if (sortedWord != lastWord)
                    {
                        anagrams = Helper.AddWordToDictionary(anagrams, sortedWord, row);
                    }
                    lastWord = sortedWord;
                }
            }
            return anagrams;
        }
    }
}
