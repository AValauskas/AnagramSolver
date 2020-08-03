using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramSolver.DatabaseLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
        public async Task<List<string>> GetAnagrams(string word)
        {
            if (word == null || word=="")
            {
                return null;
            }
            
            var spacelessWord = Regex.Replace(word, @"\s+", "");
            var sortedWord = String.Concat(spacelessWord.ToLower().OrderBy(c => c));

            var anagrams = _wordRepository.FindSingleAnagrams(sortedWord);
            var anagramsCount = Settings.AnagramCount;
            var anagramsAsString = anagrams
                    .Select(x => x.Word)
                    .Take(anagramsCount)
                    .ToList();

            return anagramsAsString;



        }
    }
}
