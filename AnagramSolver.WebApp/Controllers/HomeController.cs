using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AnagramSolver.WebApp.Models;
using AnagramSolver.Contracts.Interfaces;
using System.Threading.Tasks;
using AnagramSolver.Console.UI;
using AnagramSolver.BusinessLogic.Services;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly CookiesActions _cookies;
        public HomeController(IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
            _cookies = new CookiesActions();
        }

    
        public async Task<IActionResult> Index(string word)
        {
            List<string> anagrams;
            if (word ==null)
            {
                word = "";
            }
            if (Request.Cookies.ContainsKey(word))
            {
                anagrams = Request.Cookies[word].Split(";").ToList();
                return View(anagrams);
            }
            anagrams = await _anagramSolver.GetAnagrams(word);
            
            if (anagrams == null)
                anagrams = new List<string>();
            else
            {
                @ViewData["Anagrams"] = "Anagrams:";
                var cookie = _cookies.CreateAnagramCookie();
                var anagramsString = string.Join(";", anagrams);
                Response.Cookies.Append(word, anagramsString, cookie);
            }
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
