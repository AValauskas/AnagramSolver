using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class CachedWordService : ICachedWordService
    {
        private readonly ICachedWordRepository _cachedWordRepository;
        private readonly IWordRepository _wordRepository;
        private readonly IMapper _mapper;
        public CachedWordService(ICachedWordRepository cachedWordRepository, IWordRepository wordRepository, IMapper mapper)
        {
            _cachedWordRepository = cachedWordRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
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
            var cachedWord = await _cachedWordRepository.AddCachedWord(word);
            foreach (var anagram in anagrams)
            {
                var wordObject = await _wordRepository.GetWordByName(anagram.Word);
                await _cachedWordRepository.AddCachedWord_Word(wordObject, cachedWord);
            }
        }
        
        public async Task<IEnumerable<WordModel>> GetCachedAnagrams(string word)
        {
            var repoAnagrams = await _cachedWordRepository.GetAnagrams(word);

            var anagramList = repoAnagrams.ToList();
            var anagrams = _mapper.Map<List<WordModel>>(anagramList);
            var anagramsCount = Settings.AnagramCount;
            return anagrams
                    .Take(anagramsCount)
                    .ToList();
        }
    }
}
