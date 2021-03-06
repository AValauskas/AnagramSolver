﻿using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Interfaces.Services;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.BusinessLogic.Services
{
    public class RestrictionService: IRestrictionService
    {
        private readonly IUserLogRepository _uerLogRepository;
        public RestrictionService(IUserLogRepository uerLogRepository )
        {
            _uerLogRepository = uerLogRepository;
        }
        public async Task<bool> CheckIfActionCanBePerformed()
        {
            var ip = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList[1].ToString();
            var repoLogs = await _uerLogRepository.GetByIP(ip);

            var logsCount = repoLogs.Where(x => x.Type == TaskType.SearchAnagram || x.Type == TaskType.DeleteWord).ToList().Count -
                repoLogs.Where(x => x.Type == TaskType.UpdateWord || x.Type == TaskType.CreateWord).ToList().Count;
            
            var maxSearchCount = Settings.MaxSearchCount;
            if (maxSearchCount > logsCount)
                return true;
            return false;
          
        }
    }
}
