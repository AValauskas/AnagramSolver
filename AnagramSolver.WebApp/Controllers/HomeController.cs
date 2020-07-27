using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AnagramSolver.WebApp.Models;
using AnagramSolver.BusinessLogic;
using AnagramSolver.Contracts.Interfaces;

namespace AnagramSolver.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAnagramSolver _anagramSolver;

        public HomeController(ILogger<HomeController> logger, IAnagramSolver anagramSolver)
        {
            _anagramSolver = anagramSolver;
            _logger = logger;
        }

    
        public IActionResult Index(string id)
        {
            var anagrams = _anagramSolver.GetAnagrams(id);
            ViewData["Anagrams"] = anagrams;
            ViewData["Message"] = "Anagrams:";
            return View();
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
