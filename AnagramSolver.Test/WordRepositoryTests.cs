using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Utils;
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
        private Dictionary<string, List<Anagram>> anagrams;
        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
            anagrams = new Dictionary<string, List<Anagram>>()
            {  
                { "aabls", new List<Anagram>(){ new Anagram() { Word="labas", LanguagePart="jst"} } },
                { "iosv", new List<Anagram>(){ new Anagram() { Word="viso", LanguagePart="jst"} } },
            };
            _wordRepositoryMyDic = new WordRepository(anagrams);
        }

        [Test]
        public void ReadFile_GetData_GreaterThan0()
        {
            var words = _wordRepository.GetWords();
            Assert.Greater(words.Count, 0);
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
        [TestCase("aabls", "balas", "jst")]
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
    }
}