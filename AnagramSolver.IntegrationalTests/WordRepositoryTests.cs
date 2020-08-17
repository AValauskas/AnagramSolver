using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.EntityFramework;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace AnagramSolver.IntegrationalTests
{
    public class RollbackAttribute : Attribute, ITestAction  
    {
        private TransactionScope transaction;

        public void BeforeTest(ITest test)
        {
            transaction = new TransactionScope();  
        }

        public void AfterTest(ITest test)
        {
            transaction.Dispose();  
        }

        public ActionTargets Targets
        {
            get { return ActionTargets.Test; }  
        }
    }

    public class WordRepositoryTests
    {
        private IWordRepositoryEF _wordRepository;

        protected AnagramSolverDBContext context;
        protected IDbContextTransaction transaction;
        [SetUp]
        public void Setup()
        {          
            context = new AnagramSolverDBContext();
            transaction = context.Database.BeginTransaction();
            _wordRepository = new WordRepository(context);
        }

        [TearDown]
        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            context.Dispose();
        }

        [Test]
        public async Task AddWordToDataSet_Success()
        {
            var word = new WordEntity()
            {
                Category = "dkt",
                Word = "dara",
                SortedWord = "aadr"
            };

            await _wordRepository.AddWordToDataSet(word);
            var word2 = await _wordRepository.GetWordByName(word.Word);
            Assert.AreEqual(word.Word, word.Word);
        }
    }
}