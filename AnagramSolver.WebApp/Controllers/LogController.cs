using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.Contracts.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogService _logService;
        public LogController( ILogService logService)
        {
            _logService = logService;
        }
        public async Task<IActionResult> Index()
        {
            var logsEnum = await _logService.GetAllLogs();
            var logs = logsEnum.ToList();
            return View(logs);
        }
    }
}