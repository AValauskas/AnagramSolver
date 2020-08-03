
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

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var pageSize = Settings.PageSize;
            List<WordModel> words;

            if (!String.IsNullOrEmpty(searchString))
                 words = _wordService.SearchWordsByRangeAndFilter(pageNumber ?? 1, pageSize, searchString);
            else
             words = _wordService.GetWordsByRange(pageNumber ?? 1, pageSize);

            var totalWordsCount = 40000;//_wordService.GetTotalWordsCount();
            var paginnatedList = PaginatedList<WordModel>.Create(words, totalWordsCount, pageNumber ?? 1, pageSize);
            return View(paginnatedList);
        }
        public async Task<IActionResult> Anagrams(string word)
        {
            var anagrams = await _anagramSolver.GetAnagrams(word);
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