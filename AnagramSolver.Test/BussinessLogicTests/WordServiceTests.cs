using AnagramSolver.BusinessLogic;
using AnagramSolver.BusinessLogic.Services;
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

namespace AnagramSolver.Test
{
    public class WordServiceTests
    {
        private IWordService _wordService;
        private IWordRepository _wordRepository;

        [SetUp]
        public void Setup()
        {
            _wordRepository = new WordRepository();
            _wordService = new WordService(_wordRepository); 
        }

        [Test]
        public async Task GetDictionaryFile_GetFile_returnsFile()
        {
            var result = await _wordService.GetDictionaryFile();

            Assert.IsInstanceOf<FileStreamResult>(result);
        }


    }
}
