using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AnagramSolver.EF.DatabaseFirst;
using System.Threading.Tasks;

namespace AnagramSolver.Data.EntityFramework
{
    public class WordRepository :IWordRepository
    {
        private readonly AnagramSolverDBContext _context;

        public WordRepository(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            var wordModel = new WordModel() 
            { Word = word,
            LanguagePart = languagePart,
            SortedWord= sortedWord
            };
            _context.Words.Add(wordModel);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task AddManyWordsToDataSet(List<WordModel> words)
        {
            _context.Words.AddRange(words);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WordModel>> FindSingleWordAnagrams(string sortedWord)
        {
            var anagrams = _context.Words.Where(x => x.SortedWord == sortedWord);

            return anagrams;
        }

        public async Task<IEnumerable<WordModel>> GetAllWords()
        {
            var words = _context.Words.Where(x => true);

            return words;
        }

        public async Task<int> GetTotalWordsCount()
        {
            var count = _context.Words.Count();
            return count;
        }

        public Task<Dictionary<string, List<WordModel>>> GetWords()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WordModel>> GetWordsByRange(int pageIndex, int range)
        {
            var skip = pageIndex * range;
            var words = _context.Words
                .Where(x => true)
                .Skip(skip)
                .Take(range);
            return words;
        }

        public async Task<int> GetWordsCountBySerachedWord(string searchedWord)
        {
            var count = _context.Words
                .Where(x=> x.Word.Contains(searchedWord))
                .Count();
            return count;
        }

        public async Task<IEnumerable<WordModel>> SearchWords(string word)
        {
            var words = _context.Words
                .Where(x => x.Word.Contains(word));
            return words;
        }

        public async Task<IEnumerable<WordModel>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            //FillDatabase();
            var skip = pageIndex * range;
            var words = _context.Words
                .Where(x => x.Word
                .Contains(searchedWord))
                .Skip(skip)
                .Take(range);                
            return words;
        }
        private void FillDatabase()
        {
            IWordRepository wordRepo = new AnagramSolver.Data.WordRepository();
            var words = wordRepo.GetAllWords().Result;

            _context.Words.AddRange(words);
            _context.SaveChanges();           
        }

      
    }
}
