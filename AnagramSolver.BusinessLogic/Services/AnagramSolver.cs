using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using AnagramSolver.Contracts.Interfaces.Repositories;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepositoryEF _wordRepository;
        private readonly IMapper _mapper;
        public AnagramSolver(IWordRepositoryEF wordRepository, IMapper mapper)
        {
            _wordRepository = wordRepository;
            _mapper = mapper;
        }


        public async Task<List<WordModel>> GetAnagrams(string myWords)
        {
            if (myWords==null || myWords == "")
            {
                return null;
            }
            var spacelessWord = Regex.Replace(myWords, @"\s+", "");
            var sortedWord = String.Concat(spacelessWord.ToLower().OrderBy(c => c));

            var repoAnagrams = await _wordRepository.FindSingleWordAnagrams(sortedWord);

            var anagrams = _mapper.Map<List<WordModel>>(repoAnagrams);

            var anagramsCount = Settings.AnagramCount;
            var anagramsAsString = anagrams                    
                    .Take(anagramsCount)
                    .ToList();

            return anagramsAsString;
        }
             

    }
}
