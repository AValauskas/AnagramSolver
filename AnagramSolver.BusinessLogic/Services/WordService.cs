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
            var repoWords = await _wordRepository.GetAllWords();

            var words = new List<WordModel>();
            foreach (var word in repoWords)
            {
                words.Add(
                    new WordModel()
                    {
                        Word = word.Word1,
                        LanguagePart=word.Category,
                        SortedWord=word.SortedWord,
                        Id= word.Id
                    });
            }
            return words;
        }

        public async Task<int> GetTotalWordsCount(string searchedWord)
        {
            if (!String.IsNullOrEmpty(searchedWord))
            {
                return await _wordRepository.GetWordsCountBySerachedWord(searchedWord);
            }
            return await _wordRepository.GetTotalWordsCount();
        }

        public async Task<Dictionary<string, List<WordModel>>> GetWords()
        {
            var dictionary = new Dictionary<string, List<WordModel>>();
            var dictionaryRepo = await _wordRepository.GetWords();
           
            foreach (var words in dictionaryRepo)
            {
                var words2 = new List<WordModel>();
                foreach (var word in words.Value)
                {
                    words2.Add(new WordModel()
                    {
                        Word = word.Word1,
                        SortedWord = word.SortedWord,
                        LanguagePart = word.Category
                    });
                }
                dictionary.Add(words.Key, words2);
            }
            return dictionary;
        }

        public async Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range)
        {
            var repoWords = await _wordRepository.GetWordsByRange(pageIndex, range);

            var words = new List<WordModel>();
            foreach (var word in repoWords.ToList())
            {
                words.Add(
                    new WordModel()
                    {
                        Word = word.Word1,
                        LanguagePart = word.Category,
                        SortedWord = word.SortedWord,
                        Id = word.Id
                    });
            }
            return words;
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
            var repoWords = await _wordRepository.SearchWordsByRangeAndFilter(pageIndex, range, searchedWord);

            var words = new List<WordModel>();
            foreach (var word in repoWords)
            {
                words.Add(
                    new WordModel()
                    {
                        Word = word.Word1,
                        LanguagePart = word.Category,
                        SortedWord = word.SortedWord,
                        Id = word.Id
                    });
            }
            return words;
        }
    }
}
