using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.EntityFramework;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AnagramSolver.IntegrationalTests
{
    
    public class CachedWordRepositoryTests
    {
        private ICachedWordRepository _cachedWordRepository;
        private IWordRepositoryEF _wordRepositoryEF;
        protected AnagramSolverDBContext context;
        protected IDbContextTransaction transaction;

        private CachedWordEntity _cachedWordEntity;
        private CachedWordWord _cachedWordWord;
        private WordEntity _wordEntity;

        [SetUp]
        public void Setup()
        {
           var options = new DbContextOptionsBuilder<AnagramSolverDBContext>()
                .UseSqlServer(Settings.TestingConnectionString)
                .Options;

            context = new AnagramSolverDBContext(options);
            transaction = context.Database.BeginTransaction();
            _cachedWordRepository = new CachedWordRepositoryEF(context);
            _wordRepositoryEF = new WordRepository(context);

            _cachedWordEntity = new CachedWordEntity()
            {
                Word="sula"
            };
            _wordEntity = new WordEntity()
            {
                Category = "dkt",
                Word = "sula",
                SortedWord = "aslu"
            };
        

        }

        [TearDown]
        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            context.Dispose();
        }

        [Test]
        [TestCase("sula")]
        public async Task AddCachedWord_CachedWordInserted(string word)
        {        
            await _cachedWordRepository.AddCachedWord(word);
            await context.SaveChangesAsync();

            var cachedWord = await _cachedWordRepository.GetByWord(word);

            Assert.IsNotNull(cachedWord);
            Assert.AreEqual(word, cachedWord.ToList()[0].Word);
        }

        [Test]
        [TestCase("sula")]
        public async Task AddCachedWordWord_GetAnagrams_ReturnsAnagrams(string word)
        {
            var cachedWord = await _cachedWordRepository.AddCachedWord(word);
            await _cachedWordRepository.AddCachedWord(word);      
            await _wordRepositoryEF.AddWordToDataSet(_wordEntity);
            await context.SaveChangesAsync();

            _cachedWordWord = new CachedWordWord()
            {
                CachedWordId = cachedWord.Id,
                WordId = _wordEntity.Id,
                Word = _wordEntity,
                CachedWord = cachedWord
            };
            await _cachedWordRepository.AddCachedWord_Word(_cachedWordWord);
            await context.SaveChangesAsync();
            var anagrams = await _cachedWordRepository.GetAnagrams(word);

            Assert.IsNotNull(cachedWord);
            Assert.AreEqual(word, anagrams.ToList()[0].Word);
            Assert.AreEqual(1, anagrams.ToList().Count);
        }



    }
}