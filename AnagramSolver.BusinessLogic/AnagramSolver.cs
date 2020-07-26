using AnagramSolver.BusinessLogic.Utils;
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
        private readonly IWordRepository _wordRepository;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }
       

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

        private IList<string> GetAnagramOneWord(string myWord, string sortedWord)
        {
            var anagramsCount = Settings.GetAnagramsCount();
            var allWords = _wordRepository.GetWords();
            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord].FindAll(x => x.Word.ToLower() != myWord.ToLower())
                    .Select(x => x.Word)
                    .Take(int.Parse(anagramsCount))
                    .ToList();
            }
            return null;
        }

        private IList<string> GetAnagramFewWords(string myWords, string sortedWord)
         {
            var anagramsCount = Settings.GetAnagramsCount();
            var allWords = _wordRepository.GetWords();
            var firstDic = FirstDictionaryIteration(allWords, sortedWord);
            var secondDic = SecondDictionaryIteration(allWords, firstDic, sortedWord);

            if (secondDic.Count== 0)
            {
                return null;
            }
            var pairs = FormAllPairs(secondDic);
            if (int.Parse(anagramsCount) < pairs.Count)
                pairs.RemoveRange(int.Parse(anagramsCount), pairs.Count - int.Parse(anagramsCount));

            return pairs;
        }



        private Dictionary<string, List<List<Anagram>>> FirstDictionaryIteration(Dictionary<string, List<Anagram>> anagrams, string myWord)
        {
            Dictionary<string, List<List<Anagram>>> newDictionary = new Dictionary<string, List<List<Anagram>>>();

            foreach (var anagram in anagrams)
            {
                if (!StringProcessor.IsMatch(myWord, anagram.Key))
                    continue;
                var newString = StringProcessor.RemoveSomeLettersString(myWord, anagram.Key);
                if (newDictionary.ContainsKey(newString))
                {
                    newDictionary[newString].Add(anagram.Value);
                }
                else
                {
                    newDictionary.Add(newString, new List<List<Anagram>>() { anagram.Value });
                }
            }
            return newDictionary;
        }

        private Dictionary<string, List<List<Anagram>>> SecondDictionaryIteration(Dictionary<string, List<Anagram>> anagrams,
             Dictionary<string, List<List<Anagram>>>changedDictionary, string myWord)
        {
            var dic = new Dictionary<string, List<List<Anagram>>>();

            foreach (var item in changedDictionary)
            {
                foreach (var anagram in anagrams)
                {
                    if (anagram.Key.Length != item.Key.Length)
                        continue;
                    if (!StringProcessor.IsMatch(item.Key, anagram.Key))
                        continue;

                    if (dic.ContainsKey(item.Key))
                    {
                        dic[item.Key].Add(anagram.Value);
                    }
                    else
                    {
                        dic.Add(item.Key, item.Value);
                        dic[item.Key].Add(anagram.Value);
                    }
                }
            }
            return dic;
        }

        private List<string> FormAllPairs(Dictionary<string, List<List<Anagram>>> dictionaries)
        {
            var pairs = new List<string>();
            foreach (var item in dictionaries)
            {
                foreach (var first in item.Value[0])
                {
                    foreach (var second in item.Value[1])
                    {
                        pairs.Add(first.Word + " " + second.Word);
                    }
                }
            }
            return pairs;
        }

       
    }
}
