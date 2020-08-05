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
                var oneWordAnagrams = item.Value.Split(";").ToList();
                anagrams.Add(item.Key, oneWordAnagrams);
            }

            return View(anagrams);
        }
    }
}