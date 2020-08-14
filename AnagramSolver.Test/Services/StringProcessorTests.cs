using AnagramSolver.BusinessLogic;
using NUnit.Framework;

namespace AnagramSolver.Test
{
    public class StringProcessorTests
    {
        

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase("vismapraktika", "kava")]
        public void ISMatch_KeyContainsWord_ReturnsTrue(string key, string word)
        {
            var result = StringProcessor.IsMatch(key, word);

            Assert.IsTrue(result);
        }
        [Test]
        [TestCase("vismapraktika", "programavimas")]
        public void ISMatch_KeyNotContainsWord_ReturnsTrue(string key, string word)
        {
            var result = StringProcessor.IsMatch(key, word);

            Assert.IsFalse(result);
        }

        [Test]
        [TestCase("vismapraktika", "kava","ismprtika" )]
        public void RemoveSomeLettersString_TrimWordContainAllLetters_ReturnsNewKey(string key, string word, string expectedResult)
        {
            var result = StringProcessor.RemoveSomeLettersString(key, word);

            Assert.AreEqual(expectedResult, result);
        }
        [Test]
        [TestCase("labasrytas", "litas", "baryas")]
        public void RemoveSomeLettersString_TrimWordNotAllLettersContained_ReturnsNewKey(string key, string word, string expectedResult)
        {
            var result = StringProcessor.RemoveSomeLettersString(key, word);

            Assert.AreEqual(expectedResult, result);
        }

        [Test]
        [TestCase("labasrytas","a", "lbasrytas")]
        public void RemoveChar(string key, char letter, string expectedResult)
        {
            var result = StringProcessor.RemoveChar(key.ToCharArray(), letter);

            Assert.AreEqual(expectedResult, result);
        }


    }
}