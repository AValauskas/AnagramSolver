
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordService _wordService;
        private readonly IAnagramSolver _anagramSolver;
        private readonly ILogService _logService;

        public DictionaryController(IWordService wordService, IAnagramSolver anagramSolver, ILogService logService )
        {
            _wordService = wordService;
            _anagramSolver = anagramSolver;
            _logService = logService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var pageSize = Settings.PageSize;
            IEnumerable<WordModel> words;

            if (!String.IsNullOrEmpty(searchString))
                words = await _wordService.SearchWordsByRangeAndFilter(pageNumber ?? 1, pageSize, searchString);
            else
                words = await _wordService.GetWordsByRange(pageNumber ?? 1, pageSize);

            var totalWordsCount =await _wordService.GetTotalWordsCount(searchString);
            var paginnatedList = PaginatedList<WordModel>.Create(words, totalWordsCount, pageNumber ?? 1, pageSize);
            return View(paginnatedList);
        }
        public async Task<IActionResult> Anagrams(string word)
        {
            var anagrams = await FormAnagramView(word);

            return View(anagrams);
        }

        public async Task<IActionResult> WordAddition()
        {
          
            return View("NewWord");
        }

        [HttpPost]
        public async Task<IActionResult> OnWordWritten(string myWord, string languagePart)
        {
            if (await _wordService.AddWordToDataSet(myWord, languagePart))
            {               
                var anagrams = await FormAnagramView(myWord);
                await _logService.CreateLog(myWord, anagrams, TaskType.CreateWord);
                return View("Anagrams", anagrams);

            }
            else
            {
                @ViewData["Error"] = "Word already exist in dictionary";
                return View("NewWord");
            }
        }

        private async Task<List<string>> FormAnagramView( string word)
        {
            var anagramsobject = await _anagramSolver.GetAnagrams(word);
            var anagrams = anagramsobject.Select(x => x.Word).ToList();
            @ViewData["Word"] = word;
            if (anagrams == null || anagrams.Count == 0)
            {
                anagrams = new List<string>();
                @ViewData["Empty"] = "There is no such anagrams";
            }
            return anagrams;
        }

    }
}