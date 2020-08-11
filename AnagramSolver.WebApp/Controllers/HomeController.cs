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
using AnagramSolver.Contracts.Enums;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnagramSolver _anagramSolver;
        private readonly ILogService _logService;
        private readonly IRestrictionService _restrictionService;
        private readonly CookiesActions _cookies;
        public HomeController(IAnagramSolver anagramSolver, ILogService logService, IRestrictionService restrictionService)
        {
            _anagramSolver = anagramSolver;
            _cookies = new CookiesActions();
            _logService = logService;
            _restrictionService = restrictionService;
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
                await _logService.CreateLog(word, anagrams, TaskType.SearchAnagram);
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
                anagrams = anagramsobject.Select(x => x.Word).ToList();
                var anagramsString = string.Join(";", anagrams);
                Response.Cookies.Append(word, anagramsString, cookie);
            }
            await _logService.CreateLog(word, anagrams, TaskType.SearchAnagram);
            return View(anagrams);
        }

        [HttpPost]
        public async Task<IActionResult> OnWordWritten(string myWord)
        {
            var length = UILogic.CheckIfLengthCorrect(myWord);
            var anagrams = new List<string>();
            if (!length)
            {
                @ViewData["Error"] = "Word mus be longer";                
                return View("Index", anagrams);
            }
            if (await _restrictionService.CheckIfActionCanBePerformed())            
                return RedirectToAction("Index", new { word = myWord });
            @ViewData["Error"] = "You have used all points, if you want to search anagrams, fill our dictionary with words or update some words";
            return View("Index", anagrams);

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
