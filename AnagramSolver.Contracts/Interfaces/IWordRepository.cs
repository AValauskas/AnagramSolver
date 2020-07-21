using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IWordRepository
    {
        public Dictionary <string, List<Anagram>> ReadFile();
    }
}
