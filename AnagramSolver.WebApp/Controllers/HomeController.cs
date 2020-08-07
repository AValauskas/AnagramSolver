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
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Data;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly ILogService _logService;
        private readonly CookiesActions _cookies;
        public HomeController(IAnagramSolver anagramSolver, ILogService logService)
        {
            _anagramSolver = anagramSolver;
            _cookies = new CookiesActions();
            _logService = logService;
        }


        public async Task<IActionResult> Index(string word)
        {
            var anagrams = new List<string>();
            if (word == null)
            {
                word = "";
                return View(anagrams);
            }
            if (Request.Cookies.ContainsKey(word))
            {
                @ViewData["Anagrams"] = "Anagrams:";
                anagrams = Request.Cookies[word].Split(";").ToList();
                await _logService.CreateLog(word, anagrams);
                return View(anagrams);
            }
            var anagramsobject = await _anagramSolver.GetAnagrams(word);

            if (anagramsobject == null)
            {
                @ViewData["Anagrams"] = null;
            }
            else
            {
                @ViewData["Anagrams"] = "Anagrams:";
                var cookie = _cookies.CreateAnagramCookie();
                anagrams = anagramsobject.Select(x => x.Word1).ToList();
                var anagramsString = string.Join(";", anagrams);
                Response.Cookies.Append(word, anagramsString, cookie);
            }
            await _logService.CreateLog(word, anagrams);
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
