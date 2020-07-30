using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface IAnagramClient
    {
        public Task<List<string>> GetAnagrams(string word);
    }
}
