using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.EF.DatabaseFirst.Repositories
{
    public class WordRepository :IWordRepository
    {
        private readonly AnagramSolverDBContext _context;

        public WordRepository(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public bool AddWordToDataSet(string word, string languagePart)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> FindSingleWordAnagrams(string sortedWord)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetAllWords()
        {
            throw new NotImplementedException();
        }

        public int GetTotalWordsCount()
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<WordModel>> GetWords()
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetWordsByRange(int pageIndex, int range)
        {
            throw new NotImplementedException();
        }

        public int GetWordsCountBySerachedWord(string searchedWord)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> SearchWords(string word)
        {
            throw new NotImplementedException();
        }

        public List<WordModel> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            throw new NotImplementedException();
        }
    }
}
