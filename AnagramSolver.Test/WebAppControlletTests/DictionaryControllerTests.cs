using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class DictionaryControllerTests
    {

        private IWordRepository _wordRepositoryMock;
        private IWordService _wordServiceMock;
        private IAnagramSolver _anagramSolverMock;
        private DictionaryController _doctionaryController;
        private List<string> _words;
        private List<Anagram> _anagrams;

        [SetUp]
        public void Setup()
        {
           
            _wordRepositoryMock = Substitute.For<IWordRepository>();
            _wordServiceMock = Substitute.For<IWordService>();
            _anagramSolverMock = Substitute.For<IAnagramSolver>();
            _doctionaryController = new DictionaryController(_wordServiceMock, _anagramSolverMock);
            _anagrams = new List<Anagram>() {
                new Anagram(){ Word="alus", LanguagePart = "dkt"},
                new Anagram(){ Word="sula", LanguagePart = "dkt"},
                new Anagram(){ Word="lusa", LanguagePart = "dkt"},};
            _words = new List<string>() { "visma", "praktika" };
        }

        [Test]
        [TestCase(1)]
        public async Task Index_CallGetWordsByRange_ReceiveSignal(int pageNumber)
        {
            _wordServiceMock.GetWordsByRange(Arg.Any<int>(), Arg.Any<int>()).Returns(_anagrams);

            var result = await _doctionaryController.Index(pageNumber);

            _wordServiceMock.Received().GetWordsByRange(Arg.Any<int>(), Arg.Any<int>());
        }

        [Test]
        [TestCase(1)]
        public async Task Index_CallTotalWordsCount_ReceiveSignal(int pageNumber)
        {
            _wordServiceMock.GetWordsByRange(Arg.Any<int>(), Arg.Any<int>()).Returns(_anagrams);
            _wordServiceMock.GetTotalWordsCount().Returns(1);

            var result = await _doctionaryController.Index(pageNumber);

            _wordServiceMock.Received().GetTotalWordsCount();
        }

        [Test]
        [TestCase("visma")]
        public async Task Anagrams_CallGetAnagrams_ReceiveSignal(string myWord)
        {
           
            _anagramSolverMock.GetAnagrams(myWord).Returns(_words);

            var result = await _doctionaryController.Anagrams(myWord);

            await _anagramSolverMock.Received().GetAnagrams(Arg.Any<string>());
        }

        [Test]
        [TestCase("a")]
        public async Task Anagrams_NoAnagrams_returnViewDataEmpty(string myWord)
        {
            _anagramSolverMock.GetAnagrams(myWord).Returns((List<string>)null);

            var result =  await _doctionaryController.Anagrams(myWord) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.AreEqual("There is no such anagrams", viewData["Empty"]);
        }

        [Test]
        [TestCase("daiktas","dkt")]
        public async Task OnWordWritten_CallAddWordToDataSet_ReceiveSignal(string myWord, string languagePart)
        {
            _wordServiceMock.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var result = await _doctionaryController.OnWordWritten(myWord, languagePart);

            _wordServiceMock.Received().AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        [TestCase("daiktas", "dkt")]
        public async Task OnWordWritten_WordSuccesfulleAdded_RedirectToAnagrams(string myWord, string languagePart)
        {
            _wordServiceMock.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(true);

            var result = await _doctionaryController.OnWordWritten(myWord, languagePart) as RedirectToActionResult;

            Assert.AreEqual("Anagrams", result.ActionName);
        }

        [Test]
        [TestCase("daiktas", "dkt")]
        public async Task OnWordWritten_WordFailedToAdd_returnsViewDataError(string myWord, string languagePart)
        {
            _wordServiceMock.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var result = await _doctionaryController.OnWordWritten(myWord, languagePart) as ViewResult;
            ViewDataDictionary viewData = result.ViewData;

            Assert.AreEqual("Word already exist in dictionary", viewData["Error"]);
        }
        [Test]
        [TestCase("daiktas", "dkt")]
        public async Task OnWordWritten_WordFailedToAdd_retrurnNewWordView(string myWord, string languagePart)
        {
            _wordServiceMock.AddWordToDataSet(Arg.Any<string>(), Arg.Any<string>()).Returns(false);

            var result = await _doctionaryController.OnWordWritten(myWord, languagePart) as ViewResult;

            Assert.AreEqual("NewWord", result.ViewName);
        }

        [Test]
        [TestCase("daiktas", "dkt")]
        public async Task WordAddition_retrurnNewWordView(string myWord, string languagePart)
        {
            var result = await _doctionaryController.WordAddition() as ViewResult;

            Assert.AreEqual("NewWord", result.ViewName);
        }
    }
}
