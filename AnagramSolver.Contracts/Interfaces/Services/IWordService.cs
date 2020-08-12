using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordService
    {
        public Task<Dictionary<string, List<WordModel>>> GetWords();
        public Task<IEnumerable<WordModel>> GetAllWords();
        public Task<bool> AddWordToDataSet(string word, string languagePart);
        public Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range);
        public Task<int> GetTotalWordsCount(string searchedWord);
        public Task<FileStreamResult> GetDictionaryFile();
        public Task<IEnumerable<WordModel>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord);
        public Task DeleteWordByName(string word);
        public Task<WordModel> GetWordByName(string word);
        public Task<WordModel> GetWordByID(int id);
        public Task<bool> UpdateWord(string word, string languagePart, int id);
    }
}
