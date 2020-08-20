using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;

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
            await _context.UserLog.AddAsync(log).ConfigureAwait(false);
        }

        public async Task<IEnumerable<UserLogEntity>> GetByIP(string ip)
        {
            var logs = await _context.UserLog.Where(x => x.UserIp == ip).ToListAsync().ConfigureAwait(false);
            return logs;
        }

        public async Task<IEnumerable<UserLogEntity>> GetLogs()
        {
            var logs = await _context.UserLog.Where(x => true).ToListAsync().ConfigureAwait(false);
            return logs;
        }
        public async Task<IEnumerable<string>> GetAllIps()
        {
            var ips = await _context.UserLog
                .GroupBy(x => x.UserIp)
                .Select(x => x.Key)                
                .ToListAsync()
                .ConfigureAwait(false);
            return ips;
        }
    }
}
