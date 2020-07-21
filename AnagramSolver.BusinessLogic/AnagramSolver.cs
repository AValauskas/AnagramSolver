using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        public IWordRepository WordRepository { get; set; }

        public IList<string> GetAnagrams(string myWords)
        {
            int countSpaces = myWords.Count(Char.IsWhiteSpace);
            var spacelessWord = Regex.Replace(myWords, @"\s+", "");
            var sortedWord = String.Concat(spacelessWord.ToLower().OrderBy(c => c));

            if (countSpaces == 0) 
            {
                return GetAnagramOneWord(myWords, sortedWord);
            }
            else
            {
                return GetAnagramFewWords(myWords, sortedWord);
            }
        }

        public IList<string> GetAnagramOneWord(string myWord, string sortedWord)
        {
            var anagramsCount = Helper.GetAnagramsCount();
            var allWords = WordRepository.ReadFile();
            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord].FindAll(x => x.Word.ToLower() != myWord.ToLower())
                    .Select(x => x.Word)
                    .Take(int.Parse(anagramsCount))
                    .ToList();
            }
            return null;
        }

         public IList<string> GetAnagramFewWords(string myWords, string sortedWord)
         {
            var anagramsCount = Helper.GetAnagramsCount();
            var allWords = WordRepository.ReadFile();
            var totalLetter = sortedWord.Count();

            Dictionary<string, List<List<Anagram>>> newDictionary = new Dictionary<string, List<List<Anagram>>>();

            foreach (var word in allWords)
            { 
                var letterCount = word.Key.Length;
                Helper.CreateNewDictionary(allWords, newDictionary, totalLetter, letterCount);
            }


            return null;
        }


    }
}
