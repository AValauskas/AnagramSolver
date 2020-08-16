using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.CodeFirst.Models;
using AnagramSolver.WebApp.Profiles;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.Test.Services
{
    public class CachedWordServiceTests
    {
        private ICachedWordRepository _cachedWordRepository;
        private IWordRepositoryEF _wordRepository;
        private ICachedWordService _cachedWordService;

        private CachedWordEntity _cachedWord;
        private List<CachedWordEntity> _cachedWords;

        private WordEntity _word;
        private List<WordEntity> _words;

        private WordModel _wordModel;
        private List<WordModel> _wordsModel;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepositoryEF>();
            _cachedWordRepository = Substitute.For<ICachedWordRepository>();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            _cachedWordService = new CachedWordService(_cachedWordRepository,_wordRepository, mapper);


            _cachedWord = new CachedWordEntity()
            {
                Word = "sula"
            };
            _cachedWords = new List<CachedWordEntity>();
            

            _word = new WordEntity()
            {
                Category = "dkt",
                Word = "sula",
                SortedWord = "alsa"
            };
            _words = new List<WordEntity>();
            _words.Add(_word);

            _wordModel = new WordModel()
            {
                LanguagePart = "dkt",
                Word = "sula",
                SortedWord = "alsa"
            };
            _wordsModel = new List<WordModel>();
            _wordsModel.Add(_wordModel);
        }


        [Test]
        [TestCase("sula")]
        public async Task CheckIfCachedWordExist_WordExist_ReturnsTrue(string word)
        {
            _cachedWords.Add(_cachedWord);
            _cachedWordRepository.GetByWord(Arg.Any<string>()).Returns(_cachedWords);

            var result = await _cachedWordService.CheckIfCachedWordExist(word);

            Assert.IsTrue(result);
            await _cachedWordRepository.Received().GetByWord(Arg.Any<string>());
        }
        [Test]
        [TestCase("sula")]
        public async Task CheckIfCachedWordExist_WordNotExist_ReturnsFalse(string word)
        {
            _cachedWordRepository.GetByWord(Arg.Any<string>()).Returns(_cachedWords);

            var result = await _cachedWordService.CheckIfCachedWordExist(word);

            Assert.IsFalse(result);
            await _cachedWordRepository.Received().GetByWord(Arg.Any<string>());
        }

        [Test]
        [TestCase("sula")]
        public async Task InsertCachedWord(string word)
        {
            _cachedWordRepository.AddCachedWord(Arg.Any<string>()).Returns(_cachedWord);
            _wordRepository.GetWordByName(Arg.Any<string>()).Returns(_word);

            await _cachedWordService.InsertCachedWord(word,_wordsModel);
           
            await _cachedWordRepository.Received().AddCachedWord(Arg.Any<string>());          
            await _wordRepository.Received().GetWordByName(Arg.Any<string>());
        }

        [Test]
        [TestCase("sula")]
        public async Task GetCachedAnagrams_ReturnsAnagrams(string word)
        {
            _cachedWordRepository.GetAnagrams(Arg.Any<string>()).Returns(_words);  

            var result = await _cachedWordService.GetCachedAnagrams(word);

            await _cachedWordRepository.Received().GetAnagrams(Arg.Any<string>());
            Assert.AreEqual(1, result.ToList().Count);
        }


        [Test]
        [TestCase("sula")]
        public async Task GetCachedAnagrams_ReturnsMaxSize(string word)
        {
            var count = Settings.AnagramCount;
            for (int i = 0; i < 50; i++)
                _words.Add(_word);

            _cachedWordRepository.GetAnagrams(Arg.Any<string>()).Returns(_words);

            var result = await _cachedWordService.GetCachedAnagrams(word);
       

            await _cachedWordRepository.Received().GetAnagrams(Arg.Any<string>());
            Assert.AreEqual(count, result.ToList().Count);
        }
    }
}
