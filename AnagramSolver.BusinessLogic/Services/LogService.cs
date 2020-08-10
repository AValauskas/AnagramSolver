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
            var time = DateTime.Now;
            var ip = GetIp();

            var anagramsString = string.Join(";", anagrams.ToArray());
            var userLog = new EF.DatabaseFirst.Models.UserLog()
            {
                Anagrams = anagramsString,
                Time = time,
                UserIp = ip,
                SearchedWord = word
            };
           await _uerLogRepository.CreateLog(userLog);
        }
        public async Task<IEnumerable<UserLog>> GetAllLogs()
        {
            var repoLogs = await _uerLogRepository.GetLogs();

            var logs = new List<UserLog>();
            foreach (var log in repoLogs)
            {
                logs.Add(
                    new UserLog()
                    {
                        Anagrams = log.Anagrams,
                        Time= log.Time,
                        UserIp = log.UserIp,
                        Word=log.SearchedWord
                    });
            }
            return logs;
        }

        private string GetIp()
        {
            var ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            return ip;
        }
    }
}
