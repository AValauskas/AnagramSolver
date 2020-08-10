using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface ICachedWordRepository
    {
        public Task<CachedWord> AddCachedWord(string word);
        public Task<bool> AddCachedWord_Word(Word word, CachedWord cachedWord);
        public Task<IEnumerable<CachedWord>> GetByWord(string word);
        public Task<IEnumerable<Word>> GetAnagrams(string word);

    }
}
