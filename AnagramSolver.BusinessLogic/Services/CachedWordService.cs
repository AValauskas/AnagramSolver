using AnagramSolver.BusinessLogic.Utils;
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
        private readonly IWordRepositoryEF _wordRepository;
        private readonly IMapper _mapper;
        public CachedWordService(ICachedWordRepository cachedWordRepository, IWordRepositoryEF wordRepository, IMapper mapper)
        {
            _cachedWordRepository = cachedWordRepository;
            _wordRepository = wordRepository;
            _mapper = mapper;
        }
        public async Task<bool> CheckIfCachedWordExist(string word)
        {
            var cachedWord = await _cachedWordRepository.GetByWord(word);           
            try
            {              
                return cachedWord.Any();
            }
            catch
            {
                return false;
            }           
            
        }
        public async Task InsertCachedWord(string word, List<WordModel> anagrams)
        {
            var cachedWord = await _cachedWordRepository.AddCachedWord(word);

            foreach (var anagram in anagrams)
            {
                var wordObject = await _wordRepository.GetWordByName(anagram.Word);
                var cachedWordword = new EF.CodeFirst.Models.CachedWordWord()
                {
                    CachedWordId = cachedWord.Id,
                    WordId= wordObject.Id,
                    Word= wordObject,
                    CachedWord = cachedWord                    
                };
                await _cachedWordRepository.AddCachedWord_Word(cachedWordword);
            }
        }
        
        public async Task<IList<WordModel>> GetCachedAnagrams(string word)
        {
            var repoAnagrams = await _cachedWordRepository.GetAnagrams(word);

            var anagrams = _mapper.Map<List<WordModel>>(repoAnagrams);
            var anagramsCount = Settings.AnagramCount;

            return anagrams
                    .Take(anagramsCount)
                    .ToList();
        }
    }
}
