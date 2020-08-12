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
        public CookiesActions()
        {
          
        }
        public CookieOptions CreateAnagramCookie()
        {
            var cookies = new CookieOptions();
            cookies.Expires = DateTime.Now.AddDays(7);
            return cookies;
        }

       
    }
}
