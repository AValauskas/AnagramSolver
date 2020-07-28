using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

    
        public IActionResult Index(string word)
        {
            var anagrams = _anagramSolver.GetAnagrams(word);
            if (anagrams==null)
            {
                anagrams = new List<string>();
            }
            return View(anagrams);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
