﻿using System;

namespace AnagramSolver.Contracts.Models
{
    public class WordModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string LanguagePart {get;set;}
        public string SortedWord { get; set; }
    }
}
