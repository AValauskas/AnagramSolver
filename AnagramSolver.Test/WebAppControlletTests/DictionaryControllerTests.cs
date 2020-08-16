using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AnagramSolver.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using AnagramSolver.Contracts.Enums;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class DictionaryControllerTests
    {
        private IWordService _wordService;
        private IAnagramSolver _anagramSolver;
        private ILogService _logService;
        private IRestrictionService _restrictionService;

        private DictionaryController _dictionaryController;
        private List<string> _words;
        private WordModel _word;
        private List<WordModel> _anagrams;
        // private DictionaryController _dictionaryController;

        [SetUp]
        public void Setup()
        {
            _wordService = Substitute.For<IWordService>();
            _anagramSolver = Substitute.For<IAnagramSolver>();
            _logService = Substitute.For<ILogService>();
            _restrictionService = Substitute.For<IRestrictionService>();

            _dictionaryController = new DictionaryController(_wordService, _anagramSolver, _logService, _restrictionService);
            _word = new WordModel() { Word = "alus", LanguagePart = "dkt" };
            _anagrams = new List<WordModel>() {
                new WordModel(){ Word="alus", LanguagePart = "dkt"},
                new WordModel(){ Word="sula", LanguagePart = "dkt"},
                new WordModel(){ Word="lusa", LanguagePart = "dkt"},};
            _words = new List<string>() { "visma", "praktika" };

            var httpContext = new DefaultHttpContext();
            _dictionaryController.ControllerContext = new ControllerContext();
            _dictionaryController.ControllerContext.HttpContext = httpContext;
            _dictionaryController.ControllerContext.HttpContext.Request.Headers["device-id"] = "20317";

            var tempData = new TempDataDictionary(httpContext, Substitute.For<ITempDataProvider>());
            tempData.Add("Error", null);
            _dictionaryController.TempData = tempData;
        }

        [Test]
        [TestCase(1)]
        public async Task Index_SearchStringEmpty_GetWordsByRangeReceiveSignal(int pageNumber)
        {
            _wordService.GetWordsByRange(Arg.Any<int>(), Arg.Any<int>()).Returns(_anagrams);
            _wordService.GetTotalWordsCount(Arg.Any<string>()).Returns(50);

            var result = await _dictionaryController.Index(pageNumber, "");

            await _wordService.Received().GetTotalWordsCount(Arg.Any<string>());
            await _wordService.Received().GetWordsByRange(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        [TestCase(1, "sula")]
        public async Task Index_SearchStringEmpty_SearchWordsByRangeAndFilterReceiveSignal(int pageNumber, string searchedString)
        {
            _wordService.SearchWordsByRangeAndFilter(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>()).Returns(_anagrams);
            _wordService.GetTotalWordsCount(Arg.Any<string>()).Returns(50);

            var result = await _dictionaryController.Index(pageNumber, searchedString);

            await _wordService.Received().GetTotalWordsCount(Arg.Any<string>());
            await _wordService.Received().SearchWordsByRangeAndFilter(Arg.Any<int>(), Arg.Any<int>(), Arg.Any<string>());
        }



        [Test]
        [TestCase("visma")]
        public async Task Anagrams_CallGetAnagrams_returnAnagramsList(string myWord)
        {
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.Anagrams(myWord) as ViewResult;

            var list = result.Model as IList<string>;

            Assert.AreEqual(list.Count, 3);
        }

        [Test]
        [TestCase("a")]
        public async Task Anagrams_NoAnagrams_returnViewDataEmpty(string myWord)
        {
            _anagramSolver.GetAnagrams(myWord).Returns(new List<WordModel>());

            var result = await _dictionaryController.Anagrams(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.AreEqual("There is no such anagrams", viewData["Empty"]);
        }

        [Test]
        [TestCase("daiktas", "dkt", 3, "al")]
        public async Task OnWordWritten_InsertWordAvailable_ReturnsAnagrams(string myWord, string languagePart, int pageNumber, string searchedWord)
        {
            _wordService.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(true);
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.OnWordWritten(myWord, languagePart, null, pageNumber, searchedWord) as ViewResult; ;
            var viewData = result.ViewName;

            Assert.AreEqual(viewData, "Anagrams");
            await _wordService.Received().AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>());
            await _logService.Received().CreateLog(Arg.Any<string>(), Arg.Any<List<string>>(), Arg.Any<TaskType>());
        }

        [Test]
        [TestCase("daiktas", "dkt", 3, "al")]
        public async Task OnWordWritten_InsertWordWordExist_ReturnsError(string myWord, string languagePart, int pageNumber, string searchedWord)
        {
            _wordService.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(false);
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.OnWordWritten(myWord, languagePart, null, pageNumber, searchedWord) as ViewResult; ;
            var viewData = result.ViewData;

            Assert.AreEqual("Word already exist in dictionary", viewData["Error"]);
            await _wordService.Received().AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>());
            Assert.AreEqual("Word", result.ViewName);

        }
        [Test]
        [TestCase("daiktas", "dkt",3, 3, "al")]
        public async Task OnWordWritten_UpdateWordWordExist_ReturnsError(string myWord, string languagePart, int id, int pageNumber, string searchedWord)
        {
            _wordService.UpdateWord(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>()).Returns(false);            
            _wordService.GetWordByID(Arg.Any<int>()).Returns(_word);

            var result = await _dictionaryController.OnWordWritten(myWord, languagePart, id, pageNumber, searchedWord) as ViewResult; ;
            var viewData = result.ViewData;

            Assert.AreEqual("Word already exist in dictionary", viewData["Error"]);
            await _wordService.Received().GetWordByID(Arg.Any<int>());
      
            Assert.AreEqual("Word", result.ViewName);
        }

        [Test]
        [TestCase("daiktas", "dkt", 3, 3, "al")]
        public async Task OnWordWritten_UpdateWordNotExisting_ReturnsError(string myWord, string languagePart, int id, int pageNumber, string searchedWord)
        {
            _wordService.UpdateWord(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>()).Returns(true);
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.OnWordWritten(myWord, languagePart, id, pageNumber, searchedWord) as RedirectToActionResult;


            Assert.AreEqual("Index", result.ActionName);
            await _logService.Received().CreateLog(Arg.Any<string>(),Arg.Any<List<string>>(), Arg.Any<TaskType>());

        }


        [Test]
        [TestCase("daiktas", 3, "al")]
        public async Task DeleteWord_NoMorePoints_ReturnError(string myWord,  int pageNumber, string searchedWord)
        {
            _restrictionService.CheckIfActionCanBePerformed().Returns(false);
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.DeleteWord(myWord, pageNumber, searchedWord) as RedirectToActionResult;


            Assert.AreEqual("Index", result.ActionName);        

        }

        [Test]
        [TestCase("daiktas", 3, "al")]
        public async Task DeleteWord_SuccesfullyDeleted_ReceiveSignals(string myWord, int pageNumber, string searchedWord)
        {
            _restrictionService.CheckIfActionCanBePerformed().Returns(true);
            _anagramSolver.GetAnagrams(Arg.Any<string>()).Returns(_anagrams);

            var result = await _dictionaryController.DeleteWord(myWord, pageNumber, searchedWord) as RedirectToActionResult;

            await _wordService.Received().DeleteWordByName(Arg.Any<string>());
            await _logService.Received().CreateLog(Arg.Any<string>(), Arg.Any<List<string>>(), Arg.Any<TaskType>());
            Assert.AreEqual("Index", result.ActionName);

        }

        [Test]
        [TestCase("daiktas", "dkt")]
        public async Task WordAddition_retrurnNewWordView(string myWord, string languagePart)
        {
            var result = await _dictionaryController.WordAddition() as ViewResult;

            Assert.AreEqual("Word", result.ViewName);
        }
    }
}
