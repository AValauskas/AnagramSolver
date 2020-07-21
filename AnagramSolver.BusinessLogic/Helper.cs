using AnagramSolver.Contracts.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace AnagramSolver.BusinessLogic
{
    static class Helper
    {
        public static Dictionary<string, List<Anagram>> AddWordToDictionary(Dictionary<string, List<Anagram>> anagrams, string sortedWord, string[] row)
        {
            if (anagrams.ContainsKey(sortedWord))
            {
                anagrams[sortedWord].Add(
                    new Anagram() { 
                        Word = row[0], LanguagePart = row[1] 
                    });
            }
            else
            {
                anagrams.Add(
                    sortedWord, new List<Anagram>() { 
                        new Anagram() {
                            Word = row[0], 
                            LanguagePart = row[1] 
                        }});
            }
            return anagrams;
        }

        public static List<string> GetParameters()
        {
            var parameters = new List<string>();
            var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Path.GetDirectoryName(Environment.CurrentDirectory), @"../../../AnagramSolver.Console"))
            .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            parameters.Add(builder.Build().GetSection("anagramCount").Value);
            parameters.Add(builder.Build().GetSection("minLength").Value);
            return parameters;

           
        }
    }
}
