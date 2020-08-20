using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.DatabaseFirst;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.Data.EntityFramework
{
    public class WordRepository :IWordRepositoryEF
    {
        private readonly AnagramSolverDBContext _context;

        public WordRepository(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public async Task<bool> AddWordToDataSet(string word, string languagePart)
        {
            var sortedWord = String.Concat(word.ToLower().OrderBy(c => c));
            var wordModel = new WordEntity() 
            { Word = word,
            Category = languagePart,
            SortedWord= sortedWord
            };
            await _context.Word.AddAsync(wordModel).ConfigureAwait(false);
            return true;
        }
        public async Task AddManyWordsToDataSet(List<WordEntity> words)
        {
            await _context.Word.AddRangeAsync(words).ConfigureAwait(false);
        }

        public async Task<IEnumerable<WordEntity>> FindSingleWordAnagrams(string sortedWord)
        {
            var anagrams = await _context.Word.Where(x => x.SortedWord == sortedWord).ToListAsync().ConfigureAwait(false);

            return anagrams;
        }

        public async Task<IEnumerable<WordEntity>> GetAllWords()
        {
            var words = await _context.Word.Where(x => true).ToListAsync().ConfigureAwait(false);

            return words;
        }

        public async Task<int> GetTotalWordsCount()
        {
            var count = await _context.Word.CountAsync().ConfigureAwait(false);
            return count;
        }

        public Task<Dictionary<string, List<WordEntity>>> GetWords()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WordEntity>> GetWordsByRange(int pageIndex, int range)
        {
            var skip = (pageIndex-1) * range;
            var words = await _context.Word
                .Where(x => true)
                .Skip(skip)
                .Take(range)
                .ToListAsync().ConfigureAwait(false);
            return words;
        }

        public async Task<int> GetWordsCountBySerachedWord(string searchedWord)
        {
            var count = await _context.Word
                .Where(x=> x.Word.Contains(searchedWord))
                .CountAsync()
                .ConfigureAwait(false);
            return count;
        }

        public async Task<IEnumerable<WordEntity>> SearchWords(string word)
        {
            var words = await _context.Word
                .Where(x => x.Word.Contains(word))
                .ToListAsync()
                .ConfigureAwait(false);
            return words;
        }

        public async Task<IEnumerable<WordEntity>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
           // FillDataBase();
            var skip = (pageIndex-1) * range;
            var words = await _context.Word
                .Where(x => x.Word.StartsWith(searchedWord))
                .Skip(skip)
                .Take(range)
                .ToListAsync()
                .ConfigureAwait(false);
            return words;
        }
       
        public async Task<WordEntity> GetWordByName(string word)
        {
            var foundWord = await _context.Word.FirstOrDefaultAsync(x => x.Word == word).ConfigureAwait(false);

            return foundWord;
        }

        public async Task DeleteWordByName(string word)
        {
            var itemToRemove = await _context.Word.SingleOrDefaultAsync(x => x.Word == word).ConfigureAwait(false);

            _context.Word.Remove(itemToRemove);      
        }

        public async Task<WordEntity> GetWordById(int id)
        {
            var foundWord = await _context.Word.FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);
            return foundWord;
           
        }

        public async Task<WordEntity> UpdateWord(string word, string languagePart, int id)
        {
            var wordEntity = await GetWordById(id);
            wordEntity.Word = word;
            wordEntity.Category = languagePart;

            return wordEntity;
           
        }

        public async Task AddWordToDataSet(WordEntity word)
        {
            await _context.Word.AddAsync(word).ConfigureAwait(false);
        }

        private async Task FillDataBase()
        {
            var wordRepo = new Data.WordRepository();
            var words = await wordRepo.GetAllWords();
            var allWords = words.ToList();

            await _context.Word.AddRangeAsync(allWords).ConfigureAwait(false);
        }
    }
}
