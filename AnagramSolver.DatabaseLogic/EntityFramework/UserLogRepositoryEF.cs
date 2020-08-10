using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using AnagramSolver.EF.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnagramSolver.EF.DatabaseFirst;

namespace AnagramSolver.Data.EntityFramework
{
    public class UserLogRepositoryEF : IUserLogRepository
    {
        private readonly AnagramSolverContext _context;

        public UserLogRepositoryEF(AnagramSolverContext context)
        {
            _context = context;
        }

        public async Task CreateLog(UserLog log)
        {
            _context.UserLog.Add(log);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<UserLog>> GetLogs()
        {
            var logs = _context.UserLog.Where(x => true);
            return logs;
        }
    }
}
