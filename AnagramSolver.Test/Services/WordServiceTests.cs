using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
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
    public class WordServiceTests
    {
        private IWordService _wordService;
        private IWordRepositoryEF _wordRepository;
        private WordEntity _word;
        private List<WordEntity> _words;

        [SetUp]
        public void Setup()
        {
            _wordRepository = Substitute.For<IWordRepositoryEF>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            _wordService = new WordService(_wordRepository, mapper);
            _word = new WordEntity()
            {
                Category = "dkt",
                Word = "sula",
                SortedWord = "alsa"
            };
            _words = new List<WordEntity>();
            _words.Add(_word);
        }

        [Test]
        [TestCase("sula", "dkt")]
        public async Task AddWordToDataSet_WordExist_ReturnFalse(string word, string languagePart)
        {
            _wordRepository.GetWordByName(Arg.Any<string>()).Returns(_word);

            var result = await _wordService.AddWordToDataSet(word, languagePart);

            Assert.IsFalse(result);
            await _wordRepository.Received().GetWordByName(Arg.Any<string>());
        }

        [Test]
        [TestCase("sula", "dkt")]
        public async Task AddWordToDataSet_WordExist_SuccesfullyAdded(string word, string languagePart)
        {
            _wordRepository.GetWordByName(Arg.Any<string>()).Returns((WordEntity)null);

            var result = await _wordService.AddWordToDataSet(word, languagePart);

            Assert.IsTrue(result);
            await _wordRepository.Received().GetWordByName(Arg.Any<string>());
            await _wordRepository.Received().AddWordToDataSet(Arg.Any<WordEntity>());
        }

        [Test]
        public async Task GetAllWords_ShouldReturnWords()
        {
            _wordRepository.GetAllWords().Returns(_words);
            
            var enumerableResult = await _wordService.GetAllWords();
            var result = enumerableResult.ToList();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<WordModel>>(result);

            await _wordRepository.Received().GetAllWords();
        }

        [Test]
        [TestCase("sula")]
        public async Task GetTotalWordsCount_CountBySearchedWord_ReceiveGetWordsCountBySerachedWord(string word)
        {     
            var result = await _wordService.GetTotalWordsCount(word);

            await _wordRepository.Received().GetWordsCountBySerachedWord(Arg.Any<string>());
        }

        [Test]
        [TestCase(null)]
        public async Task GetTotalWordsCount_CountBySearchedWord_ReceiveGetTotalWordsCount(string word)
        {
            var result = await _wordService.GetTotalWordsCount(word);

            await _wordRepository.Received().GetTotalWordsCount();
        }

        [Test]
        [TestCase(0, 100)]
        public async Task GetWordsByRange_ReturnWordList(int pageIndex, int range)
        {
            _wordRepository.GetWordsByRange(Arg.Any<int>(), Arg.Any<int>()).Returns(_words);

            var enumerableResult = await _wordService.GetWordsByRange(pageIndex, range);
            var result = enumerableResult.ToList();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Contracts.Models.WordModel>>(result);
            await _wordRepository.Received().GetWordsByRange(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        public async Task GetDictionaryFile_ReturnFileStream()
        {
            var result = await _wordService.GetDictionaryFile();

            Assert.IsInstanceOf<FileStreamResult>(result);
        }

        [Test]
        [TestCase(0, 100, "sula")]
        public async Task SearchWordsByRangeAndFilter_ReturnsList(int pageIndex, int range, string word)
        {
            _wordRepository.SearchWordsByRangeAndFilter(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(_words);

            var enumerableResult = await _wordService.SearchWordsByRangeAndFilter(pageIndex, range, word);
            var result = enumerableResult.ToList();

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<List<Contracts.Models.WordModel>>(result);
            await _wordRepository.Received().SearchWordsByRangeAndFilter(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>());
        }

        [Test]
        [TestCase("sula")]
        public async Task DeleteWordByName_ReceiveSignal(string word)
        {
           await _wordService.DeleteWordByName(word);

            await _wordRepository.Received().DeleteWordByName(Arg.Any<string>());
        }

        [Test]
        [TestCase("sula","dkt",5)]
        public async Task UpdateWord_WordAlreadyExist_ReturnsFalse(string word, string category, int id)
        {
            _wordRepository.GetWordByName(Arg.Any<string>()).Returns(_word);

            var result = await _wordService.UpdateWord(word, category, id);

            Assert.IsFalse(result);
            await _wordRepository.Received().GetWordByName(Arg.Any<string>());
        }

        [Test]
        [TestCase("sula", "dkt", 5)]
        public async Task UpdateWord_WordAlreadyExist_ReturnsTrue(string word, string category, int id)
        {
            _wordRepository.GetWordByName(Arg.Any<string>()).Returns((WordEntity)null);
            _wordRepository.GetWordById(Arg.Any<int>()).Returns(_word);

            var result = await _wordService.UpdateWord(word, category, id);

            Assert.IsTrue(result);
            await _wordRepository.Received().GetWordByName(Arg.Any<string>());
            await _wordRepository.Received().GetWordById(Arg.Any<int>());
            await _wordRepository.Received().UpdateWord(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        [TestCase(5)]
        public async Task GetWordByID(int id)
        {
            _wordRepository.GetWordById(Arg.Any<int>()).Returns(_word);

            var result = await _wordService.GetWordByID(id);

            await _wordRepository.Received().GetWordById(Arg.Any<int>());
            Assert.IsInstanceOf<WordModel>(result);
        }
    }
}
