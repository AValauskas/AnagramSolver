﻿using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Services
{
    public interface ILogService
    {
        public Task CreateLog(string word, List<string> anagrams);
        Task<List<UserLog>> GetAllLogs();
    }
}