using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private readonly IUserLogRepository _uerLogRepository;
        public LogService(IUserLogRepository uerLogRepository)
        {
            _uerLogRepository = uerLogRepository;
        }

        public async Task CreateLog(string word, List<string> anagrams)
        {
            //TODO Fix ip and anagrams string
            var time = DateTime.Now;
            var ip = "22:22";
            var anagramsString = anagrams.ToString();
            var userLog = new UserLog()
            {
                Anagrams = anagramsString,
                Time = time,
                UserIp = ip,
                Word = word
            };
           await _uerLogRepository.CreateLog(userLog);
        }
        public async Task<List<UserLog>> GetAllLogs()
        {
            return await _uerLogRepository.GetLogs();
        }


    }
}
