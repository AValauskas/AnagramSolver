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
            await _context.Word.AddAsync(wordModel);
            return true;
        }
        public async Task AddManyWordsToDataSet(List<WordEntity> words)
        {
            await _context.Word.AddRangeAsync(words);
        }

        public async Task<IEnumerable<WordEntity>> FindSingleWordAnagrams(string sortedWord)
        {
            var anagrams = _context.Word.Where(x => x.SortedWord == sortedWord);

            return anagrams;
        }

        public async Task<IEnumerable<WordEntity>> GetAllWords()
        {
            var words = _context.Word.Where(x => true);

            return words;
        }

        public async Task<int> GetTotalWordsCount()
        {
            var count = _context.Word.Count();
            return count;
        }

        public Task<Dictionary<string, List<WordEntity>>> GetWords()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WordEntity>> GetWordsByRange(int pageIndex, int range)
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
                .Where(x=> x.Word.Contains(searchedWord))
                .Count();
            return count;
        }

        public async Task<IEnumerable<WordEntity>> SearchWords(string word)
        {
            var words = _context.Word
                .Where(x => x.Word.Contains(word));
            return words;
        }

        public async Task<IEnumerable<WordEntity>> SearchWordsByRangeAndFilter(int pageIndex, int range, string searchedWord)
        {
           // FillDataBase();
            var skip = (pageIndex-1) * range;
            var words = _context.Word
                .Where(x => x.Word.StartsWith(searchedWord))
                .Skip(skip)
                .Take(range);                
            return words;
        }
       
        public async Task<WordEntity> GetWordByName(string word)
        {
            var foundWord = await _context.Word.FirstOrDefaultAsync(x => x.Word == word);

            return foundWord;
        }

        public async Task DeleteWordByName(string word)
        {
            var itemToRemove = await _context.Word.SingleOrDefaultAsync(x => x.Word == word);

            _context.Word.Remove(itemToRemove);      
        }

        public async Task<WordEntity> GetWordById(int id)
        {
            var foundWord = await _context.Word.FirstOrDefaultAsync(x => x.Id == id);           
            return foundWord;
           
        }

        public async Task<WordEntity> UpdateWord(WordEntity word)
        {
           var wordEntity = _context.Word.Update(word).Entity;
            return wordEntity;
           
        }

        public async Task AddWordToDataSet(WordEntity word)
        {
            await _context.Word.AddAsync(word);         
        }

        private async Task FillDataBase()
        {
            var wordRepo = new Data.WordRepository();
            var words = await wordRepo.GetAllWords();
            var allWords = words.ToList();

            await _context.Word.AddRangeAsync(allWords);
        }
    }
}
