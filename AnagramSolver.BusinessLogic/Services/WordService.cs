using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.BusinessLogic.Services
{
    public class WordService : IWordService
    {
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
    }
}
