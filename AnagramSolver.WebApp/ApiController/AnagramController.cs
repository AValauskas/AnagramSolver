using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Console.UI;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnagramSolver.WebApp.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramController : ControllerBase
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly ICachedWordService _cachedWordService;
        private readonly ILogService _logService;
        private readonly IRestrictionService _restrictionService;
        public AnagramController(IAnagramSolver anagramSolver, ICachedWordService cachedWordService, ILogService logService, IRestrictionService restrictionService)
        {
            _anagramSolver = anagramSolver;
            _cachedWordService = cachedWordService;
            _logService = logService;
            _restrictionService = restrictionService;
        }

        [HttpGet("{word}")]
        public async Task<IActionResult> GetAnagrams([FromRoute] string word)
        {
            var length = UILogic.CheckIfLengthCorrect(word);
            if (!length)
                return BadRequest("Word is to short");

            if (!await _restrictionService.CheckIfActionCanBePerformed())
                return BadRequest("You have used all points, if you want to search anagrams, fill our dictionary with words or update some words");

            var exist = await _cachedWordService.CheckIfCachedWordExist(word);
            List<WordModel> anagrams;
            if (exist)
            {
                var enumerableAnagrams = await _cachedWordService.GetCachedAnagrams(word);
                 anagrams = enumerableAnagrams.ToList();
            }
            else
            {
                anagrams = await _anagramSolver.GetAnagrams(word);
                await _cachedWordService.InsertCachedWord(word, anagrams);
            }
            var jsonAnagrams = JsonConvert.SerializeObject(anagrams);
            return Ok(jsonAnagrams);
        }



    }
}