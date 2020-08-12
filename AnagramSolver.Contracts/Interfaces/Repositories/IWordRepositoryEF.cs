using AnagramSolver.EF.DatabaseFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface IWordRepositoryEF : IWordRepository
    {   public Task AddWordToDataSet(Word word);
        public Task AddManyWordsToDataSet(List<Word> words);
        public Task<Word> GetWordByName(string word);
        public Task DeleteWordByName(string word);
        public Task<Word> GetWordById(int id);
        public Task<Word> UpdateWord(Word word);
    }
}
