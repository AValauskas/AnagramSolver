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

        public bool AddWord(string sortedWord, string word, string languagePart)
        {
            return _wordRepository.AddWord(sortedWord, word, languagePart);
        }

        public bool AddWordToDataSet(string word, string languagePart)
        {
            return _wordRepository.AddWordToDataSet(word, languagePart);
        }

        public List<Anagram> GetAllWords()
        {
            return _wordRepository.GetAllWords();
        }

        public int GetTotalWordsCount()
        {
            return _wordRepository.GetTotalWordsCount();
        }

        public Dictionary<string, List<Anagram>> GetWords()
        {
            return _wordRepository.GetWords();
        }

        public List<Anagram> GetWordsByRange(int pageIndex, int range)
        {
            return _wordRepository.GetWordsByRange(pageIndex, range);
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

    }
}
