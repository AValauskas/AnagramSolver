using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Contracts.Models;
using AnagramSolver.EF.DatabaseFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnagramSolver.Data.EntityFramework
{
    public class UserLogRepositoryEF : IUserLogRepository
    {
        private readonly AnagramSolverDBContext _context;

        public UserLogRepositoryEF(AnagramSolverDBContext context)
        {
            _context = context;
        }

        public async Task CreateLog(UserLog log)
        {
            _context.UserLogs.Add(log);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<UserLog>> GetLogs()
        {
            var logs = _context.UserLogs.Where(x => true);
            return logs;
        }
    }
}
