using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private IAnagramSolver _anagramSolver;
        private ICachedWordService _cachedWordService;
        public AnagramController(IAnagramSolver anagramSolver, ICachedWordService cachedWordService)
        {
            _anagramSolver = anagramSolver;
            _cachedWordService = cachedWordService;
        }

        [HttpGet("{word}")]
        public async Task<IActionResult> GetAnagrams([FromRoute] string word)
        {
            var exist = await _cachedWordService.CheckIfCachedWordExist(word);
            //List<WordModel> anagramsobject;
            List<WordModel> anagrams;
            if (exist)
            {
                 anagrams = await _cachedWordService.GetCachedAnagrams(word);
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