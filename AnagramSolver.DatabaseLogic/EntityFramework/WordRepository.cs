using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.Data.EntityFramework
{
    public class WordRepository :IWordRepository
    {
        private readonly AnagramSolverContext _context;

        public WordRepository(AnagramSolverContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            var wordModel = new Word() 
            { Word1 = word,
            Category = languagePart,
            SortedWord= sortedWord
            };
            _context.Word.Add(wordModel);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task AddManyWordsToDataSet(List<Word> words)
        {
            _context.Word.AddRange(words);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Word>> FindSingleWordAnagrams(string sortedWord)
        {
            var anagrams = _context.Word.Where(x => x.SortedWord == sortedWord);

            return anagrams;
        }

        public async Task<IEnumerable<Word>> GetAllWords()
        {
            var words = _context.Word.Where(x => true);

            return words;
        }

        public async Task<int> GetTotalWordsCount()
        {
            var count = _context.Word.Count();
            return count;
        }

        public Task<Dictionary<string, List<Word>>> GetWords()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Word>> GetWordsByRange(int pageIndex, int range)
        {
            var skip = (pageIndex-1) * range;
            var words = _context.Word
                .Where(x => true)
                .Skip(skip)
                .Take(range);
            return words;
        }

        public async Task<int> GetWordsCountBySerachedWord(string searchedWord)
        {
            var count = _context.Word
                .Where(x=> x.Word1.Contains(searchedWord))
                .Count();
            return count;
        }

        public async Task<IEnumerable<Word>> SearchWords(string word)
        {
            var words = _context.Word
                .Where(x => x.Word1.Contains(word));
            return words;
        }

        public async Task<IEnumerable<Word>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
            //TODO Fix filtering  ///Contains should be different

            var skip = (pageIndex-1) * range;
            var words = _context.Word
                .Where(x => x.Word1.StartsWith(searchedWord))
                .Skip(skip)
                .Take(range);                
            return words;
        }
        private void FillDatabase()
        {
            IWordRepository wordRepo = new AnagramSolver.Data.WordRepository();
            var words = wordRepo.GetAllWords().Result;

            _context.Word.AddRange(words);
            _context.SaveChanges();           
        }

        public async Task<Word> GetWordByName(string word)
        {
            var foundWord = _context.Word.FirstOrDefault(x => x.Word1 == word);

            return foundWord;
        }

        public async Task DeleteWordByName(string word)
        {
            var itemToRemove = _context.Word.SingleOrDefault(x => x.Word1 == word);

            _context.Word.Remove(itemToRemove);
            _context.SaveChanges();
        }
    }
}
