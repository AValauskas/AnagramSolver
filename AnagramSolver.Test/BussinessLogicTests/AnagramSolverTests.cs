using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Data;
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
        private Dictionary<string, List<WordModel>> words;

        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
            _wordRepositoryMock = Substitute.For<IWordRepository>();
            _anagramSolver = new BusinessLogic.AnagramSolver(_wordRepository);
            _anagramSolverMock = new BusinessLogic.AnagramSolver(_wordRepositoryMock);

            words = new Dictionary<string, List<WordModel>>()
            {
                { "aabls", new List<WordModel>(){ 
                    new WordModel() { Word="labas", LanguagePart="jst"},
                    new WordModel() { Word="balas", LanguagePart="jst"},
                    new WordModel() { Word="salab", LanguagePart="jst"}}},
                { "iosv", new List<WordModel>(){ new WordModel() { Word="viso", LanguagePart="jst"}}},
                { "aakv", new List<WordModel>(){ new WordModel() { Word="kava", LanguagePart="dkt"}}},
                { "aiikmprst", new List<WordModel>(){ new WordModel() { Word="trikampis", LanguagePart="dkt"}}},
                { "arsyt", new List<WordModel>(){ new WordModel() { Word="tyras", LanguagePart="bdv"}}},               
                { "eiikmoprstu", new List<WordModel>(){
                    new WordModel() { Word= "sompiuterik", LanguagePart="bdv"},
                    new WordModel() { Word= "piuteriskom", LanguagePart="bdv"},
                    new WordModel() { Word= "teriskompiu", LanguagePart="bdv"},
                    new WordModel() { Word= "riskompiute", LanguagePart="bdv"},
                    new WordModel() { Word= "iuteriskomp", LanguagePart="bdv"},
                    new WordModel() { Word= "kopmiuteris", LanguagePart="bdv"},
                    new WordModel() { Word= "kompiuteris", LanguagePart="bdv"}}},
            };
        }

        [Test]
        [TestCase("Labas")]
        public async Task GetAnagrams_OneWord_Return2ElementList(string myWord)
        {
           // _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(2, anagrams.Count);
        }

        [Test]
        [TestCase("kompiuteris")]
        public async Task GetAnagrams_OneWord_ReturnMaxSizeList(string myWord)
        {
            var size = Settings.AnagramCount;
         //   _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(size, anagrams.Count);
        }

        [Test]
        [TestCase("Sveikas")]
        [TestCase("automobilis")]
        public async Task GetAnagrams_OneWord_ReturnEmptyList(string myWord)
        {
          //  _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("Laba vakara")]
        [TestCase("grazi diena")]
        public async Task GetAnagrams_TwoWords_ReturnEmptyList(string myWord)
        {
         //   _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("visma praktika")]
        public async Task GetAnagrams_TwoWords_Return1ElementList(string myWord)
        {
          //   _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(2, anagrams.Count);
        }

        [Test]
        [TestCase("Labas rytas")]
        public async Task GetAnagrams_TwoWords_ReturnMaxSizeList(string myWord)
        {
            var size = Settings.AnagramCount;
         //   _wordRepositoryMock.GetWords().Returns(words);

            var anagrams = await _anagramSolverMock.GetAnagrams(myWord);

            Assert.AreEqual(size, anagrams.Count);
        }
    }
}