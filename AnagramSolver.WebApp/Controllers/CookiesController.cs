using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class CookiesController : Controller
    {
        public IActionResult Index()
        {
            var anagrams = new Dictionary<string, List<string>>();
            foreach (var item in Request.Cookies)
            {
                anagrams.Add(item.Key, item.Value.Split(";").ToList());
            }

            return View(anagrams);
        }
    }
}