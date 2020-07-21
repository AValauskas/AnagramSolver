using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        public IWordRepository WordRepository { get; set; }
        private readonly IConfiguration _config;

        public IList<string> GetAnagrams(string myWords)
        {
            var sortedWord = String.Concat(myWords.ToLower().OrderBy(c => c));
            var anagramsCount = Helper.GetAnagramsCount();
            var allWords = WordRepository.ReadFile();

            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord].FindAll(x => x.Word.ToLower() != myWords.ToLower())
                    .Select(x => x.Word)
                    .Take(int.Parse(anagramsCount))
                    .ToList();
            }
            return null;
        }
    }
}
