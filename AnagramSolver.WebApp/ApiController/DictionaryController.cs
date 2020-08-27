using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IWordService _wordService;
        private readonly ILogService _logService;
        private readonly IAnagramSolver _anagramSolver;
        private readonly IRestrictionService _restrictionService;
        public DictionaryController(IWordService wordServie, ILogService logService, IAnagramSolver anagramSolver, IRestrictionService restrictionService)
        {
            _wordService = wordServie;
            _logService = logService;
            _anagramSolver = anagramSolver;
            _restrictionService = restrictionService;
        }

        [HttpGet()]
        public async Task<IActionResult> GetDictionaryFile()
        {
            var stream = await _wordService.GetDictionaryFile();

            if (stream == null)
                return NotFound();

            return (stream);
        }


        [HttpGet("{page}")]
        public async Task<IActionResult> GetWordsByRange(int? page)
        {
            var pageSize = Settings.PageSize;
            var words = await _wordService.GetWordsByRange(page ?? 1, pageSize);

            if (words == null)
                return NotFound();

            return Ok(words);
        }

        [HttpGet("word/{id}")]
        public async Task<IActionResult> GetWordById([FromRoute] int id)
        {
            var word = await _wordService.GetWordByID(id);

            if (word == null)
                return NotFound();

            return Ok(word);
        }

        [HttpPost()]
        public async Task<IActionResult> InsertWord([FromBody] WordModel word)
        {
            if (!await _wordService.AddWordToDataSet(word.Word, word.LanguagePart))
                return BadRequest("Word already exist in dictionary");

            var anagrams = await FormAnagrams(word.Word);
            await _logService.CreateLog(word.Word, anagrams, TaskType.CreateWord);

            return Ok();
        }

        [HttpPatch()]
        public async Task<IActionResult> UpdateWord(WordModel word)
        {
            if (!await _wordService.UpdateWord(word.Word, word.LanguagePart, word.Id))            
                return BadRequest("Word cannot be updated");

            var anagrams = await FormAnagrams(word.Word);
            await _logService.CreateLog(word.Word, anagrams, TaskType.UpdateWord);

            return Ok();
        }

        [HttpDelete("{word}")]
        public async Task<IActionResult> DeleteWord([FromRoute] string word)
        {
            if (!await _restrictionService.CheckIfActionCanBePerformed())
                return BadRequest("You don't have points to delete word, if you want to get points add or update words");

            await _wordService.DeleteWordByName(word);
            var anagrams = await FormAnagrams(word);
            await _logService.CreateLog(word, anagrams, TaskType.DeleteWord);



            return Ok();
        }

        private async Task<List<string>> FormAnagrams(string word)
        {
            var anagramsobject = await _anagramSolver.GetAnagrams(word);
            return anagramsobject.Select(x => x.Word).ToList();
        }

    }
}