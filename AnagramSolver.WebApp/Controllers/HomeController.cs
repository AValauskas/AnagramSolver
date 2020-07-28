using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Contracts.Interfaces;
using System.Threading.Tasks;
using AnagramSolver.Console.UI;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
        }

    
        public async Task<IActionResult> Index(string word)
        {
            var anagrams = _anagramSolver.GetAnagrams(word);
            if (anagrams==null)
                anagrams = new List<string>();
            else
                @ViewData["Anagrams"] = "Anagrams:";
            return View(anagrams);
        }

        [HttpPost]
        public async Task<IActionResult> OnWordWritten(string myWord)
        {
            var length = UILogic.CheckIfLengthCorrect(myWord);
            if (!length)
            {
                @ViewData["Error"] = "Word mus be longer";
                var anagrams = new List<string>();
                return View("Index", anagrams);
            }
            return RedirectToAction("Index", new { word = myWord });
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
