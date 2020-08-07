using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Data.EntityFramework
{
    public class CachedWordRepositoryEF : ICachedWordRepository
    {
        private readonly AnagramSolverDBContext _context;

        public CachedWordRepositoryEF(AnagramSolverDBContext context)
        {
            _context = context;
        }
        //public CachedWordRepositoryEF(EF.DatabaseFirst.Models. context)
        //{
        //    _context = context;
        //}
        public async Task<CachedWord> AddCachedWord(string word)
        {
            var cachedWord = new CachedWord() { Word = word };
            _context.CachedWords.Add(cachedWord);
            await _context.SaveChangesAsync();
            return cachedWord;
        }

        public async Task<bool> AddCachedWord_Word(WordModel word, CachedWord cachedWord)
        {
            var word_cachedWord = new CachedWord_WordEntity() { 
                Word = word, 
                CachedWord = cachedWord };
            _context.CachedWord_WordEntityes.Add(word_cachedWord);
            await _context.SaveChangesAsync();
           return true;
           
        }

        public async Task<IEnumerable<WordModel>> GetAnagrams(string word)
        {
            var anagrams = from cached in _context.CachedWords
                           join cachedWord_Word in _context.CachedWord_WordEntityes on cached.CachedWordId equals cachedWord_Word.Id
                           join dWord in _context.Words on cachedWord_Word.Id equals dWord.Id
                           where cached.Word == word
                           select dWord;

            return anagrams;


        }

        public async Task<IEnumerable<CachedWord>> GetByWord(string word)
        {
            var words = _context.CachedWords.Where(x => x.Word == word);

            return words;
        }
    }
}
