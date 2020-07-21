using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using NUnit.Framework;

namespace AnagramSolver.Test
{
    public class Tests
    {
        public IWordRepository wordRepository;
        [SetUp]
        public void Setup()
        {
            wordRepository = new WordRepository();
        }

        [Test]
        public void Performance_Test_WO_Buffer()
        {
            wordRepository.ReadFile();
        }
    }
}