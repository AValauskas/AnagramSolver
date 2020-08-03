
using NUnit.Framework;
using System.Collections.Generic;
using AnagramSolver.Contracts.Models;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.BusinessLogic;

namespace WordModelSolver.Test
{
    public class WordRepositoryTests
    {
        private IWordRepository _wordRepository;
        private IWordRepository _wordRepositoryMyDic;
        private Dictionary<string, List<WordModel>> words;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
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
                { "aikmpt", new List<WordModel>(){ new WordModel() { Word="piktam", LanguagePart="bdv"}}},
                { "aaikrsv", new List<WordModel>(){ new WordModel() { Word="vakaris", LanguagePart="dkt"}}},
                { "akmr", new List<WordModel>(){ new WordModel() { Word="mark", LanguagePart="dkt"}}},
                { "aaiikpstv", new List<WordModel>(){ new WordModel() { Word="apkvaisti", LanguagePart="bdv"}}},
                { "aakpsv", new List<WordModel>(){ new WordModel() { Word="kvapas", LanguagePart="dkt"}}},
                { "aiikmrt", new List<WordModel>(){ new WordModel() { Word="ritmika", LanguagePart="bdv"}}},
                { "aaimtv", new List<WordModel>(){ new WordModel() { Word="mitava", LanguagePart="dkt"}}},
                { "aikkprs", new List<WordModel>(){ new WordModel() { Word="skripka", LanguagePart="bdv"}}},
                { "eiikmoprstu", new List<WordModel>(){
                    new WordModel() { Word= "sompiuterik", LanguagePart="bdv"},
                    new WordModel() { Word= "piuteriskom", LanguagePart="bdv"},
                    new WordModel() { Word= "teriskompiu", LanguagePart="bdv"},
                    new WordModel() { Word= "riskompiute", LanguagePart="bdv"},
                    new WordModel() { Word= "iuteriskomp", LanguagePart="bdv"},
                    new WordModel() { Word= "kopmiuteris", LanguagePart="bdv"},
                    new WordModel() { Word= "kompiuteris", LanguagePart="bdv"}}},
            };
            _wordRepositoryMyDic = new WordRepository(words);
        }

        [Test]
        public void ReadFile_GetData_equals14()
        {
            var words = _wordRepositoryMyDic.GetWords();
            Assert.AreEqual(words.Count, 14);
        }

        //[Test]
        //[TestCase("aabls", "labas", "jst")]
        //[TestCase("iosv", "viso", "jst")]
        //public void AddWord_AddExistingWordDefault_AlreadyExist( string sortedWord, string word, string languagePart)
        //{
        //    bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

        //    Assert.IsFalse(isAdded);
        //}

        //[Test]
        //[TestCase("iosv", "sovi", "jst")]
        //public void AddWord_AddNewWordSameLetters_ReturnsSameSize(string sortedWord, string word, string languagePart)
        //{
        //    int sizeBeforeAction = _wordRepositoryMyDic.GetWords().Count;

        //    bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

        //    Assert.IsTrue(isAdded);
        //    Assert.AreEqual(sizeBeforeAction, _wordRepositoryMyDic.GetWords().Count);

        //}

        //[Test]
        //[TestCase("diena", "adein", "dkt")]
        //[TestCase("daina", "aadin", "dkt")]
        //public void AddWord_AddNewWordSameLetters_ReturnsDifferentSize(string sortedWord, string word, string languagePart)
        //{
        //    int sizeBeforeAction = _wordRepositoryMyDic.GetWords().Count;

        //    bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

        //    Assert.IsTrue(isAdded);
        //    Assert.Greater(_wordRepositoryMyDic.GetWords().Count, sizeBeforeAction);
        //}

        [Test]
        public void GetAllWords_GetAll_Equals22()
        {
            var wordList = _wordRepositoryMyDic.GetAllWords();

            Assert.AreEqual(wordList.Count, 22);
        }   

        [Test]
        [TestCase(1,10)]
        [TestCase(3,5)]
        public void GetWordByRange_GetByDefaultRange_EqualsPageSize(int pageIndex, int pageSize)
        {
            var wordList = _wordRepositoryMyDic.GetWordsByRange(pageIndex, pageSize);

            Assert.AreEqual(wordList.Count, pageSize);
        }

        [Test]
        [TestCase(3, 10)]
        [TestCase(4, 6)]
        public void GetWordByRange_GetByDefaultRange_WordCountLessThanPageSize(int pageIndex, int pageSize)
        {
            var wordList = _wordRepositoryMyDic.GetWordsByRange(pageIndex, pageSize);

            Assert.Less(wordList.Count, pageSize);
        }

        [Test]
        public void GetTotalWordsCount_GetCount_Equals22()
        {
            var wordCount = _wordRepositoryMyDic.GetTotalWordsCount();

            Assert.AreEqual(wordCount, 22);
        }

        [Test]
        [TestCase("kava","dkt")]
        [TestCase("ritmika","dkt")]
        public void AddWordToDataSet_AddExistingWord_ReturnsFalse(string myWord, string languagePart)
        {
            var response = _wordRepositoryMyDic.AddWordToDataSet(myWord, languagePart);

            Assert.IsFalse(response);
        }
    }
}