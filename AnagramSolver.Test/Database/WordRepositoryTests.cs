
using NUnit.Framework;
using System.Collections.Generic;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Data;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.Database;

namespace WordModelSolver.Test.Databse
{
    public class WordRepositoryTests
    {
        private IWordRepository _wordRepository;
        private ITableHandler _tableHandler;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new DatabaseWordRepository();
            _tableHandler = new TableHandler();
        }

        [Test]
        public void Testitng()
        {
            _tableHandler.CleanTables("CachedWord_Word");
        }

    }
}