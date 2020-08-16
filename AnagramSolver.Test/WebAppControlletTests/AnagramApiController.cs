using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.ApiController;
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
    public class AnagramApiController
    {
        private IAnagramSolver _anagramSolverMock;
        private ICachedWordService _cachedWordServiceMock;
        private AnagramController _anagramApiController;
        private List<string> _anagrams;
        
        [SetUp]
        public void Setup()
        {
            _cachedWordServiceMock = Substitute.For<ICachedWordService>();
            _anagramSolverMock = Substitute.For<IAnagramSolver>();
            _anagramApiController = new AnagramController(_anagramSolverMock, _cachedWordServiceMock);
            _anagrams = new List<string>() { "labas","salab","balas"};
        }

        [Test]
        [TestCase("sabal")]
        public async Task GetAnagrams_CachedAnagramsExist_ReceiveSignalGetCached(string word)
        {
            _cachedWordServiceMock.CheckIfCachedWordExist(word).Returns(true);         

            var result = await _anagramApiController.GetAnagrams(word);

            Assert.IsInstanceOf<OkObjectResult>(result);
            await _cachedWordServiceMock.Received().GetCachedAnagrams(Arg.Any<string>());

        }

        [Test]
        [TestCase("sabal")]
        public async Task GetAnagrams_CachedAnagramNotExist_ReceiveSolverSignal(string word)
        {
            _cachedWordServiceMock.CheckIfCachedWordExist(word).Returns(false);

            var result = await _anagramApiController.GetAnagrams(word);

            Assert.IsInstanceOf<OkObjectResult>(result);
            await _anagramSolverMock.Received().GetAnagrams(Arg.Any<string>());
            await _cachedWordServiceMock.Received().InsertCachedWord(Arg.Any<string>(), Arg.Any<List<WordModel>>());
        }

    }
}
