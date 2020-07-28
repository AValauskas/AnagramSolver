using AnagramSolver.BusinessLogic;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class DictionaryControllerTests
    {

        private IWordRepository _wordRepository;
        private IWordRepository _wordRepositoryMock;
        private IAnagramSolver _anagramSolver;
        private IAnagramSolver _anagramSolverMock;
        private Dictionary<string, List<Anagram>> words;
        

        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
            _wordRepositoryMock = Substitute.For<IWordRepository>();
            _anagramSolver = new BusinessLogic.AnagramSolver(_wordRepository);
            _anagramSolverMock = new BusinessLogic.AnagramSolver(_wordRepositoryMock);

        }

        [Test]
        public void CheckIfLengthCorrect_Lengt10_ReturnTrue()
        {

        }
    }
}
