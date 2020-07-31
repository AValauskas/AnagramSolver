using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.WebApp.ApiController;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace AnagramSolver.Test.WebAppControlletTests
{
    public class DictionaryApiControllerTests
    {
        private IWordService _wordService;
        private DictionaryController _dictionaryController;
        private FileStreamResult file;
        private FileStreamResult fileNull;

        [SetUp]
        public void Setup()
        {
            _wordService = Substitute.For<IWordService>();
            _dictionaryController = new DictionaryController(_wordService);
            file = new FileStreamResult(Stream.Null, "application/octet-stream");
            fileNull = null;
        }
        [Test]
        public async Task GetDictionaryFile_GetFile_ReturnsNotFound()
        {
            _wordService.GetDictionaryFile().Returns(fileNull);

            var result = await _dictionaryController.GetDictionaryFile();

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetDictionaryFile_GetFile_ReturnsFileStreamResultType()
        {
            _wordService.GetDictionaryFile().Returns(file);

            var result = await _dictionaryController.GetDictionaryFile();

            Assert.IsInstanceOf<FileStreamResult>(result);
        }
        [Test]
        public async Task GetDictionaryFile_GetFile_WordServiceReceiveSignal()
        {
            _wordService.GetDictionaryFile().Returns(file);

            var result = await _dictionaryController.GetDictionaryFile();

            await _wordService.Received().GetDictionaryFile();
        }



    }
}
