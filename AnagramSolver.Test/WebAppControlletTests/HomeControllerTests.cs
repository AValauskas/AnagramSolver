using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Enums;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class HomeControllerTests
    {
        private IAnagramSolver _anagramSolver;
        private ILogService _logService;
        private IRestrictionService _restrictionService;
        private HomeController _homeController;

        private List<string> _words;
        private WordModel _wordModel;
        private List<WordModel> _wordsModel;

        [SetUp]
        public void Setup()
        {
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _logService = Substitute.For<ILogService>();
            _restrictionService = Substitute.For<IRestrictionService>();

            _homeController = new HomeController(_anagramSolver, _logService, _restrictionService);          
            _words = new List<string>() { "visma", "praktika" };

            _wordModel = new WordModel()
            {
                LanguagePart = "dkt",
                Word = "sula",
                SortedWord = "alsa"
            };
            _wordsModel = new List<WordModel>();
            _wordsModel.Add(_wordModel);


            _homeController.ControllerContext = new ControllerContext();
            _homeController.ControllerContext.HttpContext = new DefaultHttpContext();
            _homeController.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";
        }
             

        [Test]
        [TestCase(null)]
        public async Task Index_WithoutWord_ReturnsIActionResult(string myWord)
        {          
            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual(null, viewData["Anagrams"]);

            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        [TestCase("naujas")]
        public async Task Index_NotBelongsToCookie_AnagramsNotFound(string myWord)
        {
            _anagramSolver.GetAnagrams(myWord).Returns(new List<WordModel>());

            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("No anagrams has been found", viewData["Anagrams"]);
            await _logService.Received().CreateLog(Arg.Any<string>(), Arg.Any<List<string>>(), Arg.Any<TaskType>());
        }

        [Test]
        [TestCase("naujas")]
        public async Task Index_NotBelongsToCookie_AnagramsFound(string myWord)
        {
            _anagramSolver.GetAnagrams(myWord).Returns(_wordsModel);

            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.AreEqual("Anagrams:", viewData["Anagrams"]);
            await _logService.Received().CreateLog(Arg.Any<string>(), Arg.Any<List<string>>(), Arg.Any<TaskType>());
        }
        [Test]
        [TestCase(null)]
        public async Task Index_WithWord_DontGetViewData(string myWord)
        {
            _anagramSolver.GetAnagrams(myWord).Returns((List<WordModel>)null);

            var result = await _homeController.Index(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            Assert.IsNull(viewData["Anagrams"]);
        }


        [Test]
        [TestCase("a")]
        public async Task OnWordWritten_LenghtTooShort_GetViewDataError(string myWord)
        {
            var result = await _homeController.OnWordWritten(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.AreEqual("Word mus be longer", viewData["Error"]);
        }

        [Test]
        [TestCase("alus")]
        public async Task OnWordWritten_LengthEnoughtRestrictionFalse_GivesError(string myWord)
        {
            _restrictionService.CheckIfActionCanBePerformed().Returns(false);

            var result = await _homeController.OnWordWritten(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;
            await _restrictionService.Received().CheckIfActionCanBePerformed();
            Assert.AreEqual("You have used all points, if you want to search anagrams, fill our dictionary with words or update some words", viewData["Error"]);
        }

        [Test]
        [TestCase("alus")]
        public async Task OnWordWritten_LengthEnoughtRestrictionTrue_RedirectToIndexAction(string myWord)
        {
            _restrictionService.CheckIfActionCanBePerformed().Returns(true);

            var result = await _homeController.OnWordWritten(myWord) as RedirectToActionResult;

            await _restrictionService.Received().CheckIfActionCanBePerformed();
            Assert.AreEqual("Index", result.ActionName);
        }
    }
}
