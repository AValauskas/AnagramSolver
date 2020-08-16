using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Task<Dictionary<string, List<WordEntity>>> GetWords();
        public Task<IEnumerable<WordEntity>> GetAllWords();

        public Task<bool> AddWordToDataSet(string word, string languagePart);
        public Task<IEnumerable<WordEntity>> GetWordsByRange(int pageIndex, int range);
        public Task<int> GetTotalWordsCount();
        public Task<int> GetWordsCountBySerachedWord(string searchedWord);
        public Task<IEnumerable<WordEntity>> FindSingleWordAnagrams(string sortedWord);
        public Task<IEnumerable<WordEntity>> SearchWords(string word);
        public Task<IEnumerable<WordEntity>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord);

    }
}
