using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class HomeControllerTests
    {
        private IAnagramSolver _anagramSolverMock;
        private Dictionary<string, List<Anagram>> _wordsDictionary;
        private HomeController _homeController;
        private List<string> _words;


        [SetUp]
        public void Setup()
        {
            _anagramSolverMock = Substitute.For<IAnagramSolver>();
            _homeController = new HomeController(_anagramSolverMock, new HttpContextAccessor());
            _words = new List<string>() { "visma", "praktika" };
        }

        [Test]
        [TestCase(null)]
        public async Task Index_CallGetAnagrams_ReceiveSignal(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((IList<string>)null);

            var result = await _homeController.Index(myWord);

            _anagramSolverMock.Received().GetAnagrams(Arg.Any<string>());
        }

        [Test]
        [TestCase(null)]
        public async Task Index_WithoutWord_ReturnsIActionResult(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((IList<string>)null);

            var result = await _homeController.Index(myWord);

           Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        [TestCase("naujas")]
        public async Task Index_WithWord_GetViewData(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns(_words);

            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.IsTrue(viewData["Anagrams"] == "Anagrams:");
        }
        [Test]
        [TestCase("null")]
        public async Task Index_WithWord_DontGetViewData(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((IList<string>)null);

            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.IsTrue(viewData["Anagrams"] == null);
        }


        [Test]
        [TestCase("a")]
        public async Task OnWordWritten_LenghtTooShort_GetViewDataError(string myWord)
        {
            var result = await _homeController.OnWordWritten(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.IsTrue(viewData["Error"] == "Word mus be longer");
        }

        [Test]
        [TestCase("alus")]
        public async Task OnWordWritten_LengthEnought_RedirectToIndexAction(string myWord)
        {
            var result = await _homeController.OnWordWritten(myWord) as RedirectToActionResult ;

            Assert.AreEqual("Index", result.ActionName); 
        }

    }
}
