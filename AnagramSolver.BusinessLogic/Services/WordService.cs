using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class WordService : IWordService
    {
        private const string filePath = "Files/zodynas.txt";
        private readonly IWordRepository _wordRepository;
        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            return await _wordRepository.AddWordToDataSet(word, languagePart);
        }

        public async Task<IEnumerable<WordModel>> GetAllWords()
        {
            return await _wordRepository.GetAllWords();
        }

        public async Task<int> GetTotalWordsCount(string searchedWord)
        {
            if (!String.IsNullOrEmpty(searchedWord))
            {
                return await _wordRepository.GetWordsCountBySerachedWord(searchedWord);
            }
            return await _wordRepository.GetTotalWordsCount();
        }

        public Task<Dictionary<string, List<WordModel>>> GetWords()
        {
            return _wordRepository.GetWords();
        }

        public async Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range)
        {
            return await _wordRepository.GetWordsByRange(pageIndex, range);
        }

        public async Task<FileStreamResult> GetDictionaryFile()
        {
            string path = Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, filePath));
            var stream = File.OpenRead(path);
            if (stream==null)
            {
                throw new BusinessException("File doesn't Exist");
            }
            var file = new FileStreamResult(stream, "application/octet-stream");
            file.FileDownloadName = "Zodynas.txt";
            return file;
        }

        public async Task<IEnumerable<WordModel>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            return await _wordRepository.SearchWordsByRangeAndFilter(pageIndex, range, searchedWord);
        }
    }
}
