using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class CachedWordService : ICachedWordService
    {
        private readonly ICachedWordRepository _cachedWordRepository;
        public CachedWordService(ICachedWordRepository cachedWordRepository)
        {
            _cachedWordRepository = cachedWordRepository;
        }
        public async Task<bool> CheckIfCachedWordExist(string word)
        {
            var cachedWord = await _cachedWordRepository.GetByWord(word);
            if ( cachedWord.Count == 0 )
                return false;

            return true;
        }
        public async Task InsertCachedWord(string word, List<WordModel> anagrams)
        {
            var id = await _cachedWordRepository.AddCachedWord(word);
            foreach (var anagram in anagrams)
            {
                await _cachedWordRepository.AddCachedWord_Word(id, anagram.Id);
            }
        }

        public async Task<List<string>> GetCachedAnagrams(string word)
        {

            return null;
        }
    }
}
