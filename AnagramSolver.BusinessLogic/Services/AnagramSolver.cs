using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
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

            var anagrams = new List<WordModel>();
            foreach (var repoWord in repoAnagrams)
            {
                anagrams.Add(
                    new WordModel()
                    {
                        Word = repoWord.Word1,
                        LanguagePart = repoWord.Category,
                        SortedWord = repoWord.SortedWord,
                        Id = repoWord.Id
                    });
            }

            var anagramsCount = Settings.AnagramCount;
            var anagramsAsString = anagrams                    
                    .Take(anagramsCount)
                    .ToList();

            return anagramsAsString;
        }
             

    }
}
