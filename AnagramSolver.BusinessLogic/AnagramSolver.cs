﻿using AnagramSolver.Contracts.Interfaces;
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
            var sortedWord = String.Concat(myWords.OrderBy(c => c));

            var jsonParameters = Helper.GetParameters();


            var allWords = WordRepository.ReadFile();

            if (allWords.ContainsKey(sortedWord))
            {
                return allWords[sortedWord].FindAll(x => x.Word != myWords)
                    .Select(x => x.Word)
                    .Take(int.Parse(jsonParameters[1]))
                    .ToList();
            }
            return null;
        }
    }
}
