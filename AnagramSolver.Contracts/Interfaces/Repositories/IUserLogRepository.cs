using AnagramSolver.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface IUserLogRepository
    {
        public Task CreateLog(UserLog log);
        public Task<IEnumerable<UserLog>> GetLogs();
    }
}
