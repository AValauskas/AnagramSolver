using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.ApiController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IWordService _wordService;
        public DictionaryController(IWordService wordServie)
        {
            _wordService = wordServie;
        }

    [HttpGet()]
        public async Task<IActionResult> GetDictionaryFile()
        {
            var stream = await _wordService.GetDictionaryFile();

            if (stream == null)
                return NotFound();

            return (stream);
        }
    }
}