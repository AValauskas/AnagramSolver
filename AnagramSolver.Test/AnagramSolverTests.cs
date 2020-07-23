using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Test
{
    public class AnagramSolverTests
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

            words = new Dictionary<string, List<Anagram>>()
            {
                { "aabls", new List<Anagram>(){ 
                    new Anagram() { Word="labas", LanguagePart="jst"},
                    new Anagram() { Word="balas", LanguagePart="jst"},
                    new Anagram() { Word="salab", LanguagePart="jst"}}},
                { "iosv", new List<Anagram>(){ new Anagram() { Word="viso", LanguagePart="jst"}}},
                { "aikmpt", new List<Anagram>(){ new Anagram() { Word="piktam", LanguagePart="bdv"}}},
                { "aaikrsv", new List<Anagram>(){ new Anagram() { Word="vakaris", LanguagePart="dkt"}}},
                { "aakv", new List<Anagram>(){ new Anagram() { Word="kava", LanguagePart="dkt"}}},
                { "aiikmprst", new List<Anagram>(){ new Anagram() { Word="trikampis", LanguagePart="dkt"}}},
                { "arsyt", new List<Anagram>(){ new Anagram() { Word="tyras", LanguagePart="bdv"}}},
            };
        }

        [Test]
        [TestCase("Labas")]
        [TestCase("sivo")]
        public void GetAnagrams_OneWord_ReturnListWithValues(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = _anagramSolverMock.GetAnagrams(myWord);

            Assert.Greater(anagrams.Count, 0);
        }

        [Test]
        [TestCase("Sveikas")]
        [TestCase("Kompiuteris")]
        public void GetAnagrams_OneWord_ReturnEmptyList(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("Laba vakara")]
        [TestCase("grazi diena")]
        public void GetAnagrams_TwoWords_ReturnEmptyList(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("Labas rytas")]
        [TestCase("visma praktika")]
        public void GetAnagrams_TwoWords_ReturnListWithValues(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = _anagramSolverMock.GetAnagrams(myWord);

            Assert.Greater(anagrams.Count, 0);
        }
    }
}