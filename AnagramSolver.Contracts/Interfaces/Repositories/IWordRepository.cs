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
        public Dictionary<string, List<WordModel>> GetWords();
        public List<WordModel> GetAllWords();

        bool AddWordToDataSet(string word, string languagePart);
        public List<WordModel> GetWordsByRange(int pageIndex, int range);
        public int GetTotalWordsCount();
        public int GetWordsCountBySerachedWord(string searchedWord);
        public List<WordModel> FindSingleWordAnagrams(string sortedWord);
        public List<WordModel> SearchWords(string word);
        public List<WordModel> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord);
    }
}
