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
        public Task<Dictionary<string, List<WordModel>>> GetWords();
        public Task<IEnumerable<WordModel>> GetAllWords();

        public Task<bool> AddWordToDataSet(string word, string languagePart);
        public Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range);
        public Task<int> GetTotalWordsCount();
        public Task<int> GetWordsCountBySerachedWord(string searchedWord);
        public Task<IEnumerable<WordModel>> FindSingleWordAnagrams(string sortedWord);
        public Task<IEnumerable<WordModel>> SearchWords(string word);
        public Task<IEnumerable<WordModel>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord);
        public Task AddManyWordsToDataSet(List<WordModel> words);
        public Task<WordModel> GetWordByName(string word);


    }
}
