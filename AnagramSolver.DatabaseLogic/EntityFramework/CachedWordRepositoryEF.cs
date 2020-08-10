﻿using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.EF.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnagramSolver.EF.DatabaseFirst;

namespace AnagramSolver.Data.EntityFramework
{
    public class CachedWordRepositoryEF : ICachedWordRepository
    {
        private readonly AnagramSolverContext _context;

        public CachedWordRepositoryEF(AnagramSolverContext context)
        {
            _context = context;
        }
        //public CachedWordRepositoryEF(EF.DatabaseFirst.Models. context)
        //{
        //    _context = context;
        //}
        public async Task<CachedWord> AddCachedWord(string word)
        {
            var cachedWord = new CachedWord() { Word = word};
            _context.CachedWord.Add(cachedWord);
            await _context.SaveChangesAsync();
            return cachedWord;
        }

        public async Task<bool> AddCachedWord_Word(Word word, CachedWord cachedWord)
        {
            var word_cachedWord = new CachedWordWord() { 
                Word = word, 
                CachedWord = cachedWord };
            _context.CachedWordWord.Add(word_cachedWord);
            await _context.SaveChangesAsync();
           return true;
           
        }

        public async Task<IEnumerable<Word>> GetAnagrams(string word)
        {
            var anagrams = from cached in _context.CachedWord
                           join cachedWord_Word in _context.CachedWordWord on cached.Id equals cachedWord_Word.CachedWordId
                           join dWord in _context.Word on cachedWord_Word.WordId equals dWord.Id
                           where cached.Word == word
                           select dWord;

            return anagrams;


        }

        public async Task<IEnumerable<CachedWord>> GetByWord(string word)
        {
            var words = _context.CachedWord.Where(x => x.Word == word);

            return words;
        }
    }
}
