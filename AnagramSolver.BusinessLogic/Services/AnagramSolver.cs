using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic
{
    public class AnagramSolver : IAnagramSolver
    {
        private readonly IWordRepository _wordRepository;
        public AnagramSolver(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }


        public async Task<List<string>> GetAnagrams(string myWords)
        {
            if (myWords==null)
            {
                return null;
            }
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

        private List<string> GetAnagramOneWord(string myWord, string sortedWord)
        {
            var anagramsCount = Settings.AnagramCount;
            var allWords = _wordRepository.GetWords();
            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord].FindAll(x => x.Word.ToLower() != myWord.ToLower())
                    .Select(x => x.Word)
                    .Take(anagramsCount)
                    .ToList();
            }
            return null;
        }

        private List<string> GetAnagramFewWords(string myWords, string sortedWord)
         {
            var anagramsCount = Settings.AnagramCount;
            var allWords = _wordRepository.GetWords();
            var firstDic = FirstDictionaryIteration(allWords, sortedWord);
            var secondDic = SecondDictionaryIteration(allWords, firstDic, sortedWord);

            if (secondDic.Count== 0)
            {
                return null;
            }
            var pairs = FormAllPairs(secondDic);
            if (anagramsCount < pairs.Count)
                pairs.RemoveRange(anagramsCount, pairs.Count - anagramsCount);

            return pairs;
        }



        private Dictionary<string, List<List<WordModel>>> FirstDictionaryIteration(Dictionary<string, List<WordModel>> anagrams, string myWord)
        {
            Dictionary<string, List<List<WordModel>>> newDictionary = new Dictionary<string, List<List<WordModel>>>();

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
                    newDictionary.Add(newString, new List<List<WordModel>>() { anagram.Value });
                }
            }
            return newDictionary;
        }

        private Dictionary<string, List<List<WordModel>>> SecondDictionaryIteration(Dictionary<string, List<WordModel>> anagrams,
             Dictionary<string, List<List<WordModel>>>changedDictionary, string myWord)
        {
            var dic = new Dictionary<string, List<List<WordModel>>>();

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

        private List<string> FormAllPairs(Dictionary<string, List<List<WordModel>>> dictionaries)
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
