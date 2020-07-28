using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace AnagramSolver.Test
{
    public class WordRepositoryTests
    {
        private IWordRepository _wordRepository;
        private IWordRepository _wordRepositoryMyDic;
        private Dictionary<string, List<Anagram>> words;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
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
                { "aikmpt", new List<Anagram>(){ new Anagram() { Word="piktam", LanguagePart="bdv"}}},
                { "aaikrsv", new List<Anagram>(){ new Anagram() { Word="vakaris", LanguagePart="dkt"}}},
                { "akmr", new List<Anagram>(){ new Anagram() { Word="mark", LanguagePart="dkt"}}},
                { "aaiikpstv", new List<Anagram>(){ new Anagram() { Word="apkvaisti", LanguagePart="bdv"}}},
                { "aakpsv", new List<Anagram>(){ new Anagram() { Word="kvapas", LanguagePart="dkt"}}},
                { "aiikmrt", new List<Anagram>(){ new Anagram() { Word="ritmika", LanguagePart="bdv"}}},
                { "aaimtv", new List<Anagram>(){ new Anagram() { Word="mitava", LanguagePart="dkt"}}},
                { "aikkprs", new List<Anagram>(){ new Anagram() { Word="skripka", LanguagePart="bdv"}}},
                { "eiikmoprstu", new List<Anagram>(){
                    new Anagram() { Word= "sompiuterik", LanguagePart="bdv"},
                    new Anagram() { Word= "piuteriskom", LanguagePart="bdv"},
                    new Anagram() { Word= "teriskompiu", LanguagePart="bdv"},
                    new Anagram() { Word= "riskompiute", LanguagePart="bdv"},
                    new Anagram() { Word= "iuteriskomp", LanguagePart="bdv"},
                    new Anagram() { Word= "kopmiuteris", LanguagePart="bdv"},
                    new Anagram() { Word= "kompiuteris", LanguagePart="bdv"}}},
            };
            _wordRepositoryMyDic = new WordRepository(words);
        }

        [Test]
        public void ReadFile_GetData_equals14()
        {
            var words = _wordRepositoryMyDic.GetWords();
            Assert.AreEqual(words.Count, 14);
        }

        [Test]
        [TestCase("aabls", "labas", "jst")]
        [TestCase("iosv", "viso", "jst")]
        public void AddWord_AddExistingWordDefault_AlreadyExist( string sortedWord, string word, string languagePart)
        {
            bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

            Assert.IsFalse(isAdded);
        }

        [Test]
        [TestCase("iosv", "sovi", "jst")]
        public void AddWord_AddNewWordSameLetters_ReturnsSameSize(string sortedWord, string word, string languagePart)
        {
            int sizeBeforeAction = _wordRepositoryMyDic.GetWords().Count;

            bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

            Assert.IsTrue(isAdded);
            Assert.AreEqual(sizeBeforeAction, _wordRepositoryMyDic.GetWords().Count);

        }

        [Test]
        [TestCase("diena", "adein", "dkt")]
        [TestCase("daina", "aadin", "dkt")]
        public void AddWord_AddNewWordSameLetters_ReturnsDifferentSize(string sortedWord, string word, string languagePart)
        {
            int sizeBeforeAction = _wordRepositoryMyDic.GetWords().Count;

            bool isAdded = _wordRepositoryMyDic.AddWord(sortedWord, word, languagePart);

            Assert.IsTrue(isAdded);
            Assert.Greater(_wordRepositoryMyDic.GetWords().Count, sizeBeforeAction);
        }

        [Test]
        public void GetAllWords_GetAll_Equals22()
        {
            var wordList = _wordRepositoryMyDic.GetAllWords();

            Assert.AreEqual(wordList.Count, 22);
        }
    }
}