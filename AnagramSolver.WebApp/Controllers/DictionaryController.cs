using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Models;
using AnagramSolver.WebApp.Logic;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class DictionaryController : Controller
    {
        private readonly IWordRepository _wordRepository;
        public DictionaryController(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public IActionResult Index(int? pageNumber)
        {
            var words = _wordRepository.GetAllWords();
            int pageSize = 100;
            return View(PaginatedList<Anagram>.CreateAsync(words, pageNumber ?? 1, pageSize));

        }
    }
}