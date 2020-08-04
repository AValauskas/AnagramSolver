using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface ICachedWordRepository
    {
        public Task<int> AddCachedWord(string word);
        public Task<bool> AddCachedWord_Word(int wordId, int cachedWordID);
        public Task<List<CachedWord>> GetByWord(string word);
        public Task<List<WordModel>> GetAnagrams(string word);

    }
}
