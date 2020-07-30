using AnagramSolver.Contracts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnagramSolver.BusinessLogic.Services
{
    public class CookiesActions
    {
        private readonly IHttpContextAccessor _http;

        public CookiesActions(IHttpContextAccessor http)
        {
            _http = http;
        }
        public void CreateAnagramCookie(string side, IList<string> anagrams)
        {
            CookieOptions cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(7);
            _http.HttpContext.Response.Cookies.Append(side, anagrams.ToString());
        }
    }
}
