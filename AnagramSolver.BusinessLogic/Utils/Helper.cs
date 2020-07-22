using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AnagramSolver.BusinessLogic
{
    public static class Helper
    {
        public static string GetAnagramsCount()
        {
            var parameters = new List<string>();
            var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json");

           return builder.Build().GetSection("anagramCount").Value;
        }

        public static int GetMinLength()
        {
            var parameters = new List<string>();
            var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json");

            return int.Parse(builder.Build().GetSection("minLength").Value);
        }


        public static List<string> CreateNewDictionary(Dictionary<string, List<Anagram>> anagrams,
            Dictionary<string, List<List<Anagram>>> newDictionary, int totalLetters, int letterCount)
        {
            var givenWords = new List<string>();

            foreach (var anagram in anagrams)
            {
               // letterCount += anagram.Key.Length;
                if (letterCount + anagram.Key.Length > totalLetters)
                {
                    continue;
                }
                if (letterCount+ anagram.Key.Length == totalLetters)
                {
                    givenWords.Add(anagram.Key);
                    if (newDictionary.ContainsKey(anagram.Key))
                    {
                        newDictionary[anagram.Key].Add(anagram.Value);
                    }
                    else
                    {
                        newDictionary.Add(anagram.Key, new List<List<Anagram>>() { anagram.Value });
                    }
                    continue;
                }
                var givenBack = CreateNewDictionary(anagrams, newDictionary, totalLetters, letterCount + anagram.Key.Length);
                if (givenBack.Count!=0)
                {
                    foreach (var letterMash in givenBack)
                    {
                        var newKey = String.Concat(letterMash + anagram.Key).OrderBy(c => c).ToString();
                        newDictionary[letterMash].Add(anagram.Value);
                        ChangeKey(newDictionary, letterMash, newKey);
                    }
                } 
            }
            return givenWords;
        }

        public static void ChangeKey(Dictionary<string, List<List<Anagram>>> dic,
                                      string fromKey, string toKey)
        {
            var value = dic[fromKey];
            dic.Remove(fromKey);
            dic[toKey] = value;
        }

    }
}
