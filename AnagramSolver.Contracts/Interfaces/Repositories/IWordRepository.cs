using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Dictionary<string, List<Anagram>> GetWords();
        public List<Anagram> GetAllWords();

        public bool AddWord(string sortedWord, string word, string languagePart);
        bool AddWordToDataSet(string word, string languagePart);
        public List<Anagram> GetWordsByRange(int pageIndex, int range);
        public int GetTotalWordsCount();
    }
}
