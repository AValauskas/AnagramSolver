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
        private  Dictionary<string, List<Anagram>> anagrams; 
        public WordRepository()
        {
            anagrams = new Dictionary<string, List<Anagram>>();
            ReadFile();
        }   

        private void ReadFile()
        {
            //Dictionary<string, List<Anagram>> 
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
                    sortedWord = String.Concat(row[0].ToLower().OrderBy(c => c));
                    
                    if (sortedWord != lastWord)
                    {
                        AddWord( sortedWord, row);
                    }
                    lastWord = sortedWord;
                }
            }
        }

        public Dictionary<string, List<Anagram>> GetDictionary()
        {
                return anagrams;
        }

        public void AddWord(string sortedWord, string[] row)
        {
            if (anagrams.ContainsKey(sortedWord))
            {
                anagrams[sortedWord].Add(
                    new Anagram()
                    {
                        Word = row[0],
                        LanguagePart = row[1]
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<Anagram>() {
                        new Anagram() {
                            Word = row[0],
                            LanguagePart = row[1]
                        }});
            }
        }

    }
}
