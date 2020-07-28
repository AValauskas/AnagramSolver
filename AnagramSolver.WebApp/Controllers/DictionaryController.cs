
using System.Collections.Generic;
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
            int pageSize = 100;
            return View(PaginatedList<Anagram>.Create(words, pageNumber ?? 1, pageSize));

        }
        public IActionResult Anagrams(string word)
        {
            var anagrams = _anagramSolver.GetAnagrams(word);
            @ViewData["Word"] = word;
            if (anagrams == null)
            {
                anagrams = new List<string>();
            }
            return View(anagrams);
        }
    }
}