﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AnagramSolver.BusinessLogic.Utils;
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
        public WordRepository(Dictionary<string, List<Anagram>> anagram)
        {
            anagrams = anagram;
        }

        private void ReadFile()
        {
           // string path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Contracts/Files/zodynas.txt");
            string path = Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"AnagramSolver.Contracts/Files/zodynas.txt");
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
                        AddWord( sortedWord, row[0], row[1]);
                    }
                    lastWord = sortedWord;
                }
            }
        }

        public Dictionary<string, List<Anagram>> GetWords()
        {
                return anagrams;
        }

        public bool AddWord(string sortedWord, string word, string languagePart)
        {
            if (anagrams.ContainsKey(sortedWord))
            {
                if (anagrams[sortedWord].Select(x=>x.Word).Contains(word))
                {
                    return false;
                }
                anagrams[sortedWord].Add(
                    new Anagram()
                    {
                        Word = word,
                        LanguagePart = languagePart
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<Anagram>() {
                        new Anagram() {
                            Word = word,
                            LanguagePart = languagePart
                        }});
            }
            return true;
        }

    }
}
