using AnagramSolver.EF.CodeFirst;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AnagramSolver.WebApp.Middleware
{
    public class ContextHandler
    {
        private readonly RequestDelegate _next;
        public ContextHandler(RequestDelegate next)
        {           
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext, AnagramSolverDBContext context)
        {          
           await _next(httpContext);
           await context.SaveChangesAsync();
        }
    }
}
