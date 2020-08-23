using Microsoft.AspNetCore.Http;
using System;

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
