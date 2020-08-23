using AnagramSolver.Contracts.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface ICachedWordService
    {
        public Task InsertCachedWord(string word, List<WordModel> anagrams);
        public Task<bool> CheckIfCachedWordExist(string word);
        public  Task<IList<WordModel>> GetCachedAnagrams(string word);
    }
}
