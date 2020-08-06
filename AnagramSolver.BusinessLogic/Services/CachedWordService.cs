using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Linq;
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
            var cachedWordList = cachedWord.ToList();
            if (cachedWordList.Count == 0 )
                return false;

            return true;
        }
        public async Task InsertCachedWord(string word, List<WordModel> anagrams)
        {
            var id = await _cachedWordRepository.AddCachedWord(word);
            foreach (var anagram in anagrams)
            {
                await _cachedWordRepository.AddCachedWord_Word(anagram.Id, id);
            }
        }
        
        public async Task<IEnumerable<WordModel>> GetCachedAnagrams(string word)
        {
            var anagrams = await _cachedWordRepository.GetAnagrams(word);
            var anagramsCount = Settings.AnagramCount;
            return anagrams
                    .Take(anagramsCount)
                    .ToList();
        }
    }
}
