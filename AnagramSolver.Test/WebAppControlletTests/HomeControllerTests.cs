using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class HomeControllerTests
    {
        private IAnagramSolver _anagramSolverMock;
        private Dictionary<string, List<Anagram>> words;
        private HomeController _homeController;


        [SetUp]
        public void Setup()
        {
            _anagramSolverMock = Substitute.For<IAnagramSolver>();
            _homeController = new HomeController(_anagramSolverMock);

        }

        [Test]
        [TestCase(null)]
        public async Task Index_ReturnsAViewResult_WithoutWord(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((IList<string>)null);

            var result = await _homeController.Index(myWord);

           Assert.IsInstanceOf<IActionResult>(result);
        }


        [Test]
        [TestCase("alus")]
        public async Task OnWordWritten(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((IList<string>)null);

            var result = await _homeController.OnWordWritten(myWord);

         //   Assert.That(result.["action"], Is.EqualTo("Index"));
        }
    }
}
