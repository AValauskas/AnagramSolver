
using NUnit.Framework;
using System.Collections.Generic;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Data;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst.Models;
using System.Linq;

namespace WordModelSolver.Test.Repositories
{
    public class WordRepositoryTests
    {
        private IWordRepository _wordRepository;
        private IWordRepository _wordRepositoryMyDic;
        private Dictionary<string, List<WordEntity>> words;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
            words = new Dictionary<string, List<WordEntity>>()
            {
                { "aabls", new List<WordEntity>(){
                    new WordEntity() { Word="labas", Category="jst"},
                    new WordEntity() { Word="balas", Category="jst"},
                    new WordEntity() { Word="salab", Category="jst"}}},
                { "iosv", new List<WordEntity>(){ new WordEntity() { Word="viso", Category = "jst"}}},
                { "aakv", new List<WordEntity>(){ new WordEntity() { Word="kava", Category = "dkt"}}},
                { "aiikmprst", new List<WordEntity>(){ new WordEntity() { Word="trikampis", Category = "dkt"}}},
                { "arsyt", new List<WordEntity>(){ new WordEntity() { Word="tyras", Category = "bdv"}}},
                { "aikmpt", new List<WordEntity>(){ new WordEntity() { Word="piktam", Category = "bdv"}}},
                { "aaikrsv", new List<WordEntity>(){ new WordEntity() { Word="vakaris", Category = "dkt"}}},
                { "akmr", new List<WordEntity>(){ new WordEntity() { Word="mark", Category = "dkt"}}},
                { "aaiikpstv", new List<WordEntity>(){ new WordEntity() { Word="apkvaisti", Category = "bdv"}}},
                { "aakpsv", new List<WordEntity>(){ new WordEntity() { Word="kvapas", Category = "dkt"}}},
                { "aiikmrt", new List<WordEntity>(){ new WordEntity() { Word="ritmika", Category = "bdv"}}},
                { "aaimtv", new List<WordEntity>(){ new WordEntity() { Word="mitava", Category = "dkt"}}},
                { "aikkprs", new List<WordEntity>(){ new WordEntity() { Word="skripka", Category = "bdv"}}},
                { "eiikmoprstu", new List<WordEntity>(){
                    new WordEntity() { Word= "sompiuterik", Category="bdv"},
                    new WordEntity() { Word= "piuteriskom", Category="bdv"},
                    new WordEntity() { Word= "teriskompiu", Category="bdv"},
                    new WordEntity() { Word= "riskompiute", Category="bdv"},
                    new WordEntity() { Word= "iuteriskomp", Category="bdv"},
                    new WordEntity() { Word= "kopmiuteris", Category="bdv"},
                    new WordEntity() { Word= "kompiuteris", Category="bdv"}}},
            };
            _wordRepositoryMyDic = new WordRepository(words);
        }

        [Test]
        public async Task ReadFile_GetData_equals14()
        {
            var words = await _wordRepositoryMyDic.GetWords();
            Assert.AreEqual(words.Count, 14);
        }

        [Test]
        public async Task GetAllWords_GetAll_Equals22()
        {
            var wordList = await _wordRepositoryMyDic.GetAllWords();
            var words = wordList.ToList();
            Assert.AreEqual(words.Count, 22);
        }   

        [Test]
        [TestCase(1,10)]
        [TestCase(3,5)]
        public async Task GetWordByRange_GetByDefaultRange_EqualsPageSize(int pageIndex, int pageSize)
        {
            var wordList = await _wordRepositoryMyDic.GetWordsByRange(pageIndex, pageSize);
            var words = wordList.ToList();

            Assert.AreEqual(words.Count, pageSize);
        }

        [Test]
        [TestCase(3, 10)]
        [TestCase(4, 6)]
        public async Task GetWordByRange_GetByDefaultRange_WordCountLessThanPageSize(int pageIndex, int pageSize)
        {
            var wordList =await _wordRepositoryMyDic.GetWordsByRange(pageIndex, pageSize);

            var words = wordList.ToList();

            Assert.Less(words.Count, pageSize);
        }

        [Test]
        public async Task GetTotalWordsCount_GetCount_Equals22()
        {
            var wordCount = await _wordRepositoryMyDic.GetTotalWordsCount();

            Assert.AreEqual(wordCount, 22);
        }

        [Test]
        [TestCase("kava","dkt")]
        [TestCase("ritmika","dkt")]
        public async Task AddWordToDataSet_AddExistingWord_ReturnsFalse(string myWord, string languagePart)
        {
            var response = await _wordRepositoryMyDic.AddWordToDataSet(myWord, languagePart);

            Assert.IsFalse(response);
        }
    }
}