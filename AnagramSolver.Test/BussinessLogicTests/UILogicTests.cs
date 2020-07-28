using AnagramSolver.Console.UI;
using NUnit.Framework;

namespace AnagramSolver.Test
{
    public class UILogicTests
    {
        

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        [TestCase("Labasrytas")]
        public void CheckIfLengthCorrect_Lengt10_ReturnTrue (string myWord)
        {
            var result = UILogic.CheckIfLengthCorrect(myWord);
            Assert.IsTrue(result);
        }
        [Test]
        [TestCase("La")]
        public void CheckIfLengthCorrect_Length2_ReturnFalse(string myWord)
        {
            var result = UILogic.CheckIfLengthCorrect(myWord);
            Assert.IsFalse(result);
        }
    }
}