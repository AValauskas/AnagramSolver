using AnagramSolver.Console.UI;
using NUnit.Framework;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class HomeControlletTests
    {

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        [TestCase("Labasrytas")]
        public void CheckIfLengthCorrect_Lengt10_ReturnTrue(string myWord)
        {
            var result = UILogic.CheckIfLengthCorrect(myWord);
            Assert.IsTrue(result);
        }
    }
}
