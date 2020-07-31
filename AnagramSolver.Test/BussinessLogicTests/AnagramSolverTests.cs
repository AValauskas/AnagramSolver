using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

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
                { "aakv", new List<Anagram>(){ new Anagram() { Word="kava", LanguagePart="dkt"}}},
                { "aiikmprst", new List<Anagram>(){ new Anagram() { Word="trikampis", LanguagePart="dkt"}}},
                { "arsyt", new List<Anagram>(){ new Anagram() { Word="tyras", LanguagePart="bdv"}}},               
                { "eiikmoprstu", new List<Anagram>(){
                    new Anagram() { Word= "sompiuterik", LanguagePart="bdv"},
                    new Anagram() { Word= "piuteriskom", LanguagePart="bdv"},
                    new Anagram() { Word= "teriskompiu", LanguagePart="bdv"},
                    new Anagram() { Word= "riskompiute", LanguagePart="bdv"},
                    new Anagram() { Word= "iuteriskomp", LanguagePart="bdv"},
                    new Anagram() { Word= "kopmiuteris", LanguagePart="bdv"},
                    new Anagram() { Word= "kompiuteris", LanguagePart="bdv"}}},
            };
        }

        [Test]
        [TestCase("Labas")]
        public async Task GetAnagrams_OneWord_Return2ElementList(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(2, anagrams.Count);
        }

        [Test]
        [TestCase("kompiuteris")]
        public async Task GetAnagrams_OneWord_ReturnMaxSizeList(string myWord)
        {
            var size = int.Parse(Settings.GetAnagramsCount());
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(size, anagrams.Count);
        }

        [Test]
        [TestCase("Sveikas")]
        [TestCase("automobilis")]
        public async Task GetAnagrams_OneWord_ReturnEmptyList(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("Laba vakara")]
        [TestCase("grazi diena")]
        public async Task GetAnagrams_TwoWords_ReturnEmptyList(string myWord)
        {
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("visma praktika")]
        public async Task GetAnagrams_TwoWords_Return1ElementList(string myWord)
        {
             _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(2, anagrams.Count);
        }

        [Test]
        [TestCase("Labas rytas")]
        public async Task GetAnagrams_TwoWords_ReturnMaxSizeList(string myWord)
        {
            var size = int.Parse(Settings.GetAnagramsCount());
            _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(size, anagrams.Count);
        }
    }
}