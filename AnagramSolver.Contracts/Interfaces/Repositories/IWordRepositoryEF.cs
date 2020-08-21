using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface IWordRepositoryEF : IWordRepository
    {   public Task AddWordToDataSet(WordEntity word);
        public Task AddManyWordsToDataSet(List<WordEntity> words);
        public Task<WordEntity> GetWordByName(string word);
        public Task DeleteWordByName(string word);
        public Task<WordEntity> GetWordById(int id);
        public Task<WordEntity> UpdateWord(string word, string languagePart, int id);
        public Task<int> FillDataBase();
    }
}
