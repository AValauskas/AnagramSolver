
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
        private readonly IRestrictionService _restrictionService;

        public DictionaryController(IWordService wordService, IAnagramSolver anagramSolver, ILogService logService, IRestrictionService restrictionService )
        {
            _wordService = wordService;
            _anagramSolver = anagramSolver;
            _logService = logService;
            _restrictionService = restrictionService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string searchString)
        {
            ViewData["CurrentFilter"] = searchString;
            var pageSize = Settings.PageSize;
            IEnumerable<WordModel> words;

            if (TempData["Error"] != null)
            {
                @ViewData["Error"] = TempData["Error"];
            }            

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
            return View("Word");
        }

        [HttpPost]
        public async Task<IActionResult> OnWordWritten(string myWord, string languagePart, int? id, int pageNumber, string searchString)
        {
            if (id == null)
                return await InsertWord(myWord, languagePart);
            else
                return await UpdateWord(myWord, languagePart, id.Value, pageNumber, searchString);


        }
        private async Task<IActionResult> InsertWord(string myWord, string languagePart)
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
                return View("Word");
            }
        }

        private async Task<IActionResult> UpdateWord(string myWord, string languagePart, int id, int pageNumber, string searchString)
        {
            if (!await _wordService.UpdateWord(myWord, languagePart, id))
            {
                @ViewData["Error"] = "Word already exist in dictionary";
                var word = await _wordService.GetWordByID(id);
                return await OpenUpdateWordView(word, pageNumber, searchString);
            }
            
            var anagrams = await FormAnagramView(myWord);
            await _logService.CreateLog(myWord, anagrams, TaskType.UpdateWord);
            return RedirectToAction("Index", new {  pageNumber, searchString });
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
        public async Task<IActionResult> DeleteWord(string word, int? pageNumber, string searchString)
        {
            if (!await _restrictionService.CheckIfActionCanBePerformed())
            {
                TempData["Error"] = "You don't have points to delete word, if you want to get points add or update words";
                return RedirectToAction("Index", new { pageNumber, searchString });
            }

            await _wordService.DeleteWordByName(word);
            var anagrams = await FormAnagramView(word);
            await _logService.CreateLog(word, anagrams, TaskType.DeleteWord);

            return RedirectToAction("Index", new {pageNumber, searchString });
        }
        public async Task<IActionResult> OpenUpdateWordView(WordModel wordModel, int? pageNumber, string searchString)
        {           
            @ViewData["Name"] = wordModel.Word;
            @ViewData["Category"] = wordModel.LanguagePart;
            @ViewData["Id"] = wordModel.Id;
            @ViewData["PageNumber"] = pageNumber;
            @ViewData["SearchString"] = searchString;
            return View("Word");
        }

    }
}