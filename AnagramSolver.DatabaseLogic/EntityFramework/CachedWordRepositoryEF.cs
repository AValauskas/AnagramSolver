using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.CodeFirst;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.Data.EntityFramework
{
    public class CachedWordRepositoryEF : ICachedWordRepository
    {
        private readonly AnagramSolverDBContext _context;

        public CachedWordRepositoryEF(AnagramSolverDBContext context)
        {
            _context = context;
        }
        public async Task<CachedWordEntity> AddCachedWord(string word)
        {
            var cachedWord = new CachedWordEntity() { Word = word };
            await _context.CachedWord.AddAsync(cachedWord);
            return cachedWord;
        }

        public async Task<bool> AddCachedWord_Word(CachedWordWord cachedWordWord)
        {
           await _context.CachedWordWord.AddAsync(cachedWordWord);
           return true;
           
        }

        public async Task<IEnumerable<WordEntity>> GetAnagrams(string word)
        {
            var anagrams = from cached in _context.CachedWord
                           join cachedWord_Word in _context.CachedWordWord on cached.Id equals cachedWord_Word.CachedWordId
                           join dWord in _context.Word on cachedWord_Word.WordId equals dWord.Id
                           where cached.Word == word
                           select dWord;

            return anagrams;


        }

        public async Task<IEnumerable<CachedWordEntity>> GetByWord(string word)
        {
            var words = _context.CachedWord.Where(x => x.Word == word);

            return words;
        }
    }
}
