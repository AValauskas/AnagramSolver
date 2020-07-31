using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces
{
    public interface IAnagramSolver
    {
        public Task<List<string>> GetAnagrams(string word);

    }
}
