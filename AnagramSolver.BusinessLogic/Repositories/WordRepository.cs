﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;

namespace AnagramSolver.BusinessLogic
{
    public class WordRepository : IWordRepository
    {
        private readonly string filePath;
        private  Dictionary<string, List<Anagram>> anagrams; 
        public WordRepository()
        {
            anagrams = new Dictionary<string, List<Anagram>>();
            filePath = Settings.FilePath;
            ReadFile();

        }
        public WordRepository(Dictionary<string, List<Anagram>> anagram)
        {
            anagrams = anagram;
        }

        private void ReadFile()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
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
        private void WriteFile(string word, string languagePart)
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteAsync("\n"+word+"\t" + languagePart);
            }
        }
        public bool AddWordToDataSet(string word, string languagePart)
        {
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            if (AddWord(sortedWord, word, languagePart))
                WriteFile(word, languagePart);
            else
                return false;

            return true;
        }

        public Dictionary<string, List<Anagram>> GetWords()
        {
                return anagrams;
        }
        public List<Anagram> GetAllWords()
        {
            return anagrams.Values.ToList().SelectMany(x => x).ToList();
        }

        public List<Anagram> GetWordsByRange(int pageIndex, int range)
        {
            var allWordList = GetAllWords();
            return allWordList.Skip((pageIndex - 1) * range).Take(range).ToList();
        }

        public int GetTotalWordsCount()
        {
            return GetAllWords().Count;
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
                        LanguagePart = languagePart,
                        SortedWord= sortedWord
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<Anagram>() {
                        new Anagram() {
                            Word = word,
                            LanguagePart = languagePart,
                            SortedWord= sortedWord
                        }});
            }
            return true;
        }
    }
}
