using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AnagramSolver.WebApp.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramController : ControllerBase
    {
        private IWordService _wordService;
        private IAnagramSolver _anagramSolver;
        public AnagramController(IWordService wordService, IAnagramSolver anagramSolver)
        {
            _wordService = wordService;
            _anagramSolver = anagramSolver;
        }

        [HttpGet("{word}")]
        public async Task<IActionResult> GetAnagrams([FromRoute] string word)
        {
            var anagrams = _anagramSolver.GetAnagrams(word);
            var jsonAnagrams = JsonConvert.SerializeObject(anagrams);

            return Ok(jsonAnagrams);
        }



    }
}