using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnagramSolver.EF.DatabaseFirst;

namespace AnagramSolver.Data.EntityFramework
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
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            var wordModel = new WordModel() 
            { Word = word,
            LanguagePart = languagePart,
            SortedWord= sortedWord
            };
            _context.Words.Add(wordModel);
            _context.SaveChanges();
            return true;
        }
        public void AddManyWordsToDataSet(List<WordModel> words)
        {
            _context.Words.AddRange(words);
            _context.SaveChanges();
        }

        public List<WordModel> FindSingleWordAnagrams(string sortedWord)
        {
            var anagrams = _context.Words.Where(x => x.SortedWord == sortedWord).ToList();

            return anagrams;
        }

        public List<WordModel> GetAllWords()
        {
            var words = _context.Words.Where(x => true).ToList();

            return words;
        }

        public int GetTotalWordsCount()
        {
            var count = _context.Words.Count();
            return count;
        }

        public Dictionary<string, List<WordModel>> GetWords()
        {
            throw new NotImplementedException();
        }

        public List<WordModel> GetWordsByRange(int pageIndex, int range)
        {
            var skip = pageIndex * range;
            var words = _context.Words
                .Where(x => true)
                .Skip(skip)
                .Take(range)
                .ToList();
            return words;
        }

        public int GetWordsCountBySerachedWord(string searchedWord)
        {
            var count = _context.Words
                .Where(x=> x.Word.Contains(searchedWord))
                .Count();
            return count;
        }

        public List<WordModel> SearchWords(string word)
        {
            var words = _context.Words
                .Where(x => x.Word.Contains(word))
                .ToList();
            return words;
        }

        public List<WordModel> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            //FillDatabase();
            var skip = pageIndex * range;
            var words = _context.Words
                .Where(x => x.Word
                .Contains(searchedWord))
                .Skip(skip)
                .Take(range)
                .ToList();
            return words;
        }
        private void FillDatabase()
        {
            IWordRepository wordRepo = new AnagramSolver.Data.WordRepository();
            var words = wordRepo.GetAllWords();

            _context.Words.AddRange(words);
            _context.SaveChanges();           
        }
    }
}
