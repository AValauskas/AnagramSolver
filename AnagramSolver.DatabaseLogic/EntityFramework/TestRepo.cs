using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Data.EntityFramework
{
    public class TestRepo
    {
        private readonly AnagramSolverDBContext _context;

        public TestRepo(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public async Task Test()
        {
            var word = new WordEntity()
            {
                Category = "rkt",
                SortedWord = "aaaa",
                Word = "aaaaa",
            };
            _context.Word.Add(word);
            var cachedWord = new CachedWordEntity()
            {               
                Word = "aaaaa",
            };
            _context.CachedWord.Add(cachedWord);
            await _context.SaveChangesAsync();
            var cachedWordWord = new CachedWordWord()
            {
                WordId= word.Id,
                CachedWordId= cachedWord.Id                
            };
            _context.CachedWordWord.Add(cachedWordWord);
            await _context.SaveChangesAsync();

        }
    }
}
