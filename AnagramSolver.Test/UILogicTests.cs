using AnagramSolver.BusinessLogic;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;

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
        public void CheckIfLengthCorrect_Lengt10_Returntrue (string myWord)
        {
            var result = UILogic.CheckIfLengthCorrect(myWord);
            Assert.IsTrue(result);
        }
        [Test]
        [TestCase("La")]
        public void CheckIfLengthCorrect_Length2_Returnfalse(string myWord)
        {
            var result = UILogic.CheckIfLengthCorrect(myWord);
            Assert.IsFalse(result);
        }
    }
}