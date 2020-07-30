
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordService _wordService;
        private readonly IAnagramSolver _anagramSolver;
      
        public DictionaryController(IWordService wordService, IAnagramSolver anagramSolver )
        {
            _wordService = wordService;
            _anagramSolver = anagramSolver;           
        }

        public async Task<IActionResult> Index(int? pageNumber)
        {
            var pageSize = Settings.GetPageSize();
            var words = _wordService.GetWordsByRange(pageNumber ?? 1, pageSize);
            var totalWordsCount = _wordService.GetTotalWordsCount();
            var paginnatedList = PaginatedList<Anagram>.Create(words, totalWordsCount, pageNumber ?? 1, pageSize);
            return View(paginnatedList);
        }
        public async Task<IActionResult> Anagrams(string word)
        {
            var anagrams = _anagramSolver.GetAnagrams(word);
            @ViewData["Word"] = word;
            if (anagrams == null || anagrams.Count == 0)
            {
                anagrams = new List<string>();
                @ViewData["Empty"] = "There is no such anagrams" ;
            }
            return View(anagrams);
        }

        public async Task<IActionResult> WordAddition()
        {
            return View("NewWord");
        }

        [HttpPost]
        public async Task<IActionResult> OnWordWritten(string myWord, string languagePart)
        {
            if (_wordService.AddWordToDataSet(myWord, languagePart))
                return RedirectToAction("Anagrams", new { word = myWord });
            else
            {
                @ViewData["Error"] = "Word already exist in dictionary";
                return View("NewWord");
            }
        }
    }
}