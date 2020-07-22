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
            var allWords = WordRepository.GetDictionary();
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
            var allWords = WordRepository.GetDictionary();
            var firstDic = FirstDictionaryIteration(allWords, sortedWord);
            var finalAnagram = new List<List<List<Anagram>>>();
            FormAnagramsDictionary(allWords, firstDic, finalAnagram, sortedWord);
            //   var pairs = FormAllPairs(secondDic);
            // pairs.RemoveRange(int.Parse(anagramsCount), pairs.Count - int.Parse(anagramsCount));
            //  return pairs;
            return null;
        }

        /*public IList<string> GetAnagramFewWords(string myWords, string sortedWord)
        {
           var anagramsCount = Helper.GetAnagramsCount();
           var allWords = WordRepository.GetDictionary();
           var firstDic = FirstDictionaryIteration(allWords, sortedWord);
           var secondDic = SecondDictionaryIteration(allWords, firstDic, sortedWord);

           var pairs = FormAllPairs(secondDic);
          // pairs.RemoveRange(int.Parse(anagramsCount), pairs.Count - int.Parse(anagramsCount));
           return pairs;
       }*/

        private void FormAnagramsDictionary(Dictionary<string, List<Anagram>> anagrams,
            Dictionary<string, List<List<Anagram>>> anagramSets,
             List<List<List<Anagram>>> finalAnagram, string myWord)
        {
            var dic = new Dictionary<string, List<List<Anagram>>>();
            foreach (var item in anagramSets)
            {
                foreach (var anagram in anagrams)
                {
                    if (anagram.Key.Length > item.Key.Length)
                        continue;
                    if (!IsMatch(item.Key, anagram.Key))
                        continue;
                    var newKey = RemoveString(myWord, anagram.Key);
                    if (dic.ContainsKey(newKey))
                    {
                        dic[item.Key].Add(anagram.Value);
                    }
                    else
                    {
                        dic.Add(item.Key, item.Value);
                        dic[item.Key].Add(anagram.Value);
                    }
                    if (anagram.Key.Length == item.Key.Length)
                    {
                        finalAnagram.Add(dic[item.Key]);
                    }
                    else
                    {
                        FormAnagramsDictionary(anagrams, dic, finalAnagram, myWord);
                    }

                }
            }
        }


    private Dictionary<string, List<List<Anagram>>> FirstDictionaryIteration(Dictionary<string, List<Anagram>> anagrams, string myWord)
        {
            Dictionary<string, List<List<Anagram>>> newDictionary = new Dictionary<string, List<List<Anagram>>>();

            foreach (var anagram in anagrams)
            {
                if (!IsMatch(myWord, anagram.Key))
                    continue;
                var newKey = RemoveString(myWord, anagram.Key);
                if (newDictionary.ContainsKey(newKey))
                {
                    newDictionary[newKey].Add(anagram.Value);
                }
                else
                {
                    newDictionary.Add(newKey, new List<List<Anagram>>() { anagram.Value });
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
                    if (!IsMatch(item.Key, anagram.Key))
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

        private bool IsMatch(string input, string word)
        {
            var myChar = word.ToCharArray();
            for (int i = 0; i < input.Length; i++)
            {
                if (myChar.Contains(input[i]))
                    myChar=RemoveChar(myChar, input[i].ToString());
            }
            if (myChar.Length == 0)
                return true;

            return false;
        }

        private string RemoveString(string input, string word)
        {
            var myChar = word.ToCharArray();
            var sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                if (!myChar.Contains(input[i]))
                {
                    sb.Append(input[i]);
                }
                else
                {
                    myChar = RemoveChar(myChar, input[i].ToString());
                }
            }
            return sb.ToString();
        }

        private char[] RemoveChar(char[] myChar, string letter)
        {
            string str = new string(myChar);
            int index = str.IndexOf(letter);
            str = str.Remove(index, 1);
            myChar = str.ToCharArray();
            return myChar;
        }
    }
}
