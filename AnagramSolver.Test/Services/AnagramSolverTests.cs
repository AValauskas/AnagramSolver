using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.WebApp.Profiles;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Test.Services
{
    public class AnagramSolverTests
    {
        private IWordRepositoryEF _wordRepository;
        private IAnagramSolver _anagramSolver;
        private List<WordEntity> words;
        private WordEntity word;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepositoryEF>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            _anagramSolver = new BusinessLogic.AnagramSolver(_wordRepository, mapper);

             word = new WordEntity()
            {
                Category = "dkt",
                SortedWord = "alsu",
                Word = "sula",
                Id = 3
            };
            words = new List<WordEntity>();
        }

        [Test]
        [TestCase("")]
        public async Task GetAnagrams_Empty_ReturnsNull(string myWord)
        {           
            var anagrams = await _anagramSolver.GetAnagrams(myWord);

            Assert.IsNull(anagrams);
        }

        [Test]
        [TestCase("alsu")]
        public async Task GetAnagrams_OneWord_Return2ElementList(string myWord)
        {
            words.Add(word);
            words.Add(word);
            _wordRepository.FindSingleWordAnagrams(myWord).Returns(words);

            var anagrams = await _anagramSolver.GetAnagrams(myWord);

            Assert.AreEqual(2, anagrams.Count);
        }

        [Test]
        [TestCase("kompiuteris")]
        public async Task GetAnagrams_OneWord_ReturnMaxSizeList(string myWord)
        {
            var size = Settings.AnagramCount;
            for (int i = 0; i < 50; i++)
                words.Add(word);

            _wordRepository.FindSingleWordAnagrams(Arg.Any<string>()).Returns(words);

            var anagrams = await _anagramSolver.GetAnagrams(myWord);

            Assert.AreEqual(size, anagrams.Count);
        }

 

  
    }
}