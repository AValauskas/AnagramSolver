
using NUnit.Framework;
using System.Collections.Generic;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic;
using AnagramSolver.DatabaseLogic;

namespace WordModelSolver.Test.Databse
{
    public class WordRepositoryTests
    {
        private IWordRepository _wordRepository;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new DatabaseWordRepository();
        }

        [Test]
        public void Testitng()
        {
            var words = _wordRepository.SearchWords("al");
        }

    }
}