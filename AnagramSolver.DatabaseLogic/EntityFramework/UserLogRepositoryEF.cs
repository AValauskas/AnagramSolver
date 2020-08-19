using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst.Models;

namespace AnagramSolver.Data.EntityFramework
{
    public class UserLogRepositoryEF : IUserLogRepository
    {
        private readonly AnagramSolverDBContext _context;

        public UserLogRepositoryEF(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public async Task CreateLog(UserLogEntity log)
        {
            await _context.UserLog.AddAsync(log);
        }

        public async Task<IEnumerable<UserLogEntity>> GetByIP(string ip)
        {
            var logs = _context.UserLog.Where(x => x.UserIp == ip);
            return logs;
        }

        public async Task<IEnumerable<UserLogEntity>> GetLogs()
        {
            var logs = _context.UserLog.Where(x => true);
            return logs;
        }
        public async Task<IEnumerable<string>> GetAllIps()
        {
            var ips = _context.UserLog
                .GroupBy(x => x.UserIp)
                .Select(x => x.Key);                
                
            return ips;
        }
    }
}
