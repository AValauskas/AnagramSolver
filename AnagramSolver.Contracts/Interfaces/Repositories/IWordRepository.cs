using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Task<Dictionary<string, List<Word>>> GetWords();
        public Task<IEnumerable<Word>> GetAllWords();

        public Task<bool> AddWordToDataSet(string word, string languagePart);
        public Task<IEnumerable<Word>> GetWordsByRange(int pageIndex, int range);
        public Task<int> GetTotalWordsCount();
        public Task<int> GetWordsCountBySerachedWord(string searchedWord);
        public Task<IEnumerable<Word>> FindSingleWordAnagrams(string sortedWord);
        public Task<IEnumerable<Word>> SearchWords(string word);
        public Task<IEnumerable<Word>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord);
        public Task AddManyWordsToDataSet(List<Word> words);
        public Task<Word> GetWordByName(string word);


    }
}
