using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.EntityFramework;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AnagramSolver.IntegrationalTests
{
    
    public class WordRepositoryTests
    {
        private IWordRepositoryEF _wordRepository;
        private  List<WordEntity> _words;
        private WordEntity _wordEntity;
        protected AnagramSolverDBContext context;
        protected IDbContextTransaction transaction;

        [SetUp]
        public void Setup()
        {
           var options = new DbContextOptionsBuilder<AnagramSolverDBContext>()
                .UseSqlServer(Settings.TestingConnectionString)
                .Options;

            context = new AnagramSolverDBContext(options);
            transaction = context.Database.BeginTransaction();
            _wordRepository = new WordRepository(context);

            _words = new List<WordEntity>()
            { new WordEntity()
            {
                Category = "dkt",
                Word = "alus",
                SortedWord = "aslu"
            },
                new WordEntity()
            {
                Category = "dkt",
                Word = "sula",
                SortedWord = "aslu"
            },
            new WordEntity()
            {
                Category = "dkt",
                Word = "visma",
                SortedWord = "aimsv"
            } };

            _wordEntity = new WordEntity()
            {
                Category = "dkt",
                Word = "dara",
                SortedWord = "aadr"
            };
        }

        [TearDown]
        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            context.Dispose();
        }

        [Test]
        public async Task AddWordToDataSet_GetWordEqualsInsertedword()
        {        
            await _wordRepository.AddWordToDataSet(_wordEntity);
            await context.SaveChangesAsync();

            var databaseWord = await _wordRepository.GetWordByName(_wordEntity.Word);

            Assert.IsNotNull(databaseWord.Id);
            Assert.AreEqual(databaseWord.Word, _wordEntity.Word);
        }

        [Test]
        [TestCase("sula", "dkt")]
        public async Task AddWordToDataSet_NotObject_GetWordEqualsInsertedword(string word, string category)
        {
            await _wordRepository.AddWordToDataSet(word, category);
            await context.SaveChangesAsync();

            var databaseWord = await _wordRepository.GetWordByName(word);

            Assert.IsNotNull(databaseWord.Id);
            Assert.AreEqual(word,databaseWord.Word);
        }
        [Test]
        public async Task AddManyWordsToDataSet_GetCount_Returns3()
        {
            await _wordRepository.AddManyWordsToDataSet(_words);

            await context.SaveChangesAsync();
            var count = await _wordRepository.GetTotalWordsCount();
          
            Assert.AreEqual(3, count);
        }
        [Test]
        [TestCase("aslu")]
        public async Task FindSingleWordAnagrams_ReturnAnagrams(string sortedWord)
        {
            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.FindSingleWordAnagrams(sortedWord);
            var anagrams = anagramsRepo.ToList();

            Assert.AreEqual(2, anagrams.Count);
            Assert.AreEqual(sortedWord, anagrams[0].SortedWord);
        }

        [Test]
        [TestCase(1,5)]
        public async Task GetWordsByRange_ReturnMaxCountWords(int pageIndex, int range)
        {
            for (int i = 0; i < 50; i++)
            {
                var wordE = new WordEntity();
                wordE.Word = _wordEntity.Word + i.ToString();
                wordE.Category = _wordEntity.Category + i.ToString();
                wordE.Category = _wordEntity.SortedWord + i.ToString();
                _words.Add(wordE);
            }

            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.GetWordsByRange(pageIndex, range);
            var anagrams = anagramsRepo.ToList();

            Assert.AreEqual(range, anagrams.Count);        
        }

        [Test]
        [TestCase(1, 5)]
        public async Task GetWordsByRange_ReturnDefaultCountWords(int pageIndex, int range)
        {
            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.GetWordsByRange(pageIndex, range);
            var anagrams = anagramsRepo.ToList();

            Assert.AreEqual(_words.Count, anagrams.Count);
        }

        [Test]
        [TestCase("al")]
        public async Task GetWordsCountBySerachedWord_Returns2(string searchedWord)
        {
            var word = new WordEntity()
            {
                Category = "dkt",
                Word = "alu",
                SortedWord = "aslu"
            };
            _words.Add(word);
            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.GetWordsCountBySerachedWord(searchedWord);

            var count = anagramsRepo;
            Assert.AreEqual(2, count);
        }

        [Test]
        [TestCase(1,3,"al")]
        public async Task SearchWordsByRangeAndFilter_ReturnMaxSearchedWords(int pageIndex, int range, string searchedWord)
        {
            for (int i = 0; i < 50; i++)
            {
                var word = new WordEntity()
                {
                    Category = "dkt" + i.ToString(),
                    Word = "alu" + i.ToString(),
                    SortedWord = "aslu" + i.ToString()
                };
                _words.Add(word);
            }            
            
            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.SearchWordsByRangeAndFilter(pageIndex, range, searchedWord);

            var anagrams = anagramsRepo.ToList(); ;
            Assert.AreEqual(range, anagrams.Count);
            Assert.IsTrue(anagrams[0].Word.StartsWith(searchedWord));
        }

        [Test]
        [TestCase(1, 3, "al")]
        public async Task SearchWordsByRangeAndFilter_ReturnDefaultWordsCount(int pageIndex, int range, string searchedWord)
        {
            var word = new WordEntity()
            {
                Category = "dkt",
                Word = "alu",
                SortedWord = "aslu"
            };
            _words.Add(word);
            await _wordRepository.AddManyWordsToDataSet(_words);
            await context.SaveChangesAsync();

            var anagramsRepo = await _wordRepository.SearchWordsByRangeAndFilter(pageIndex, range, searchedWord);

            var anagrams = anagramsRepo.ToList(); ;
            Assert.AreEqual(2, anagrams.Count);
            Assert.IsTrue(anagrams[0].Word.StartsWith(searchedWord));
        }

        [Test]
        [TestCase(1, 3, "al")]
        public async Task GetWordById_returnsWord(int pageIndex, int range, string searchedWord)
        {            
            await _wordRepository.AddWordToDataSet(_wordEntity);
            await context.SaveChangesAsync();

            var wordRepo = await _wordRepository.GetWordById(_wordEntity.Id);
       
            Assert.AreEqual(_wordEntity.Word, wordRepo.Word);
       
        }

        [Test]
        public async Task UpdateWord_RetursUpdatedWord()
        {
            await _wordRepository.AddWordToDataSet(_wordEntity);
            await context.SaveChangesAsync();
            _wordEntity.Word += "2";

            var wordRepo = await _wordRepository.UpdateWord(_wordEntity.Word, _wordEntity.Category, _wordEntity.Id);

            Assert.AreEqual(_wordEntity.Word, wordRepo.Word);

        }
    }
}