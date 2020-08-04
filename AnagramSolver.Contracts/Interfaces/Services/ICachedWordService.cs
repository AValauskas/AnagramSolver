using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface ICachedWordService
    {
        public Task InsertCachedWord(string word, List<WordModel> anagrams);
        public Task<bool> CheckIfCachedWordExist(string word);
        public  Task<List<string>> GetCachedAnagrams(string word);
    }
}
