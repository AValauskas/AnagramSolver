using AnagramSolver.EF.CodeFirst.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface ICachedWordRepository
    {
        public Task<CachedWordEntity> AddCachedWord(string word);
        public Task<bool> AddCachedWord_Word(CachedWordWord cachedWordword);
        public Task<IEnumerable<CachedWordEntity>> GetByWord(string word);
        public Task<IEnumerable<WordEntity>> GetAnagrams(string word);

    }
}
