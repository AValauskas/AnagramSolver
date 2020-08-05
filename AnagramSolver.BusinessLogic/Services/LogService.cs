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
            //TODO Fix ip
            var time = DateTime.Now;
            var ip = GetIp();

            var anagramsString = string.Join(";", anagrams.ToArray());
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

        private string GetIp()
        {
            var ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            return ip;
        }
    }
}
