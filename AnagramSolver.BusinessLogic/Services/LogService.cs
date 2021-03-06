﻿using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using AnagramSolver.Contracts.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class LogService : ILogService
    {
        private readonly IUserLogRepository _uerLogRepository;
        private readonly IMapper _mapper;
        public LogService(IUserLogRepository uerLogRepository, IMapper mapper)
        {
            _uerLogRepository = uerLogRepository;
            _mapper = mapper;
        }

        public async Task CreateLog(string word, List<string> anagrams, TaskType type)
        {
            var time = DateTime.Now;
            var ip = GetIp();
            string anagramsString;

            if (anagrams==null)            
                anagramsString = "";
           else
            anagramsString = string.Join(";", anagrams.ToArray());

            var userLog = new EF.CodeFirst.Models.UserLogEntity()
            {
                Anagrams = anagramsString,
                Time = time,
                UserIp = ip,
                SearchedWord = word,
                Type= type
            };
           await _uerLogRepository.CreateLog(userLog);
        }

        public async Task<IEnumerable<string>> GetAllIpsOfLogs()
        {
            var ips =  await _uerLogRepository.GetAllIps();
            return ips;
        }

        public async Task<IEnumerable<UserLog>> GetAllLogs()
        {
            var repoLogs = await _uerLogRepository.GetLogs();
            var logs = _mapper.Map<List<UserLog>>(repoLogs);           
            return logs;
        }

        private string GetIp()
        {
            var ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            return ip;
        }
    }
}
