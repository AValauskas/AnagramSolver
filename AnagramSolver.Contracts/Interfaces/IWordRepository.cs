using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Dictionary<string, List<Anagram>> GetWords();
        public List<Anagram> GetAllWords();

        public bool AddWord(string sortedWord, string word, string languagePart);
        bool AddWordToDataSet(string word, string languagePart);
    }
}
