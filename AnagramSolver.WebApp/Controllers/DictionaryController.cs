
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAnagramSolver _anagramSolver;
        public DictionaryController(IWordRepository wordRepository, IAnagramSolver anagramSolver)
        {
            _wordRepository = wordRepository;
            _anagramSolver = anagramSolver;
        }

        public IActionResult Index(int? pageNumber)
        {
            var words = _wordRepository.GetAllWords();
            int pageSize = Settings.GetPageSize();
            return View(PaginatedList<Anagram>.Create(words, pageNumber ?? 1, pageSize));
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
            if (_wordRepository.AddWordToDataSet(myWord, languagePart))
                return RedirectToAction("Anagrams", new { word = myWord });
            else
            {
                @ViewData["Error"] = "Word already exist in dictionary";
                return View("NewWord");
            }
        }
    }
}