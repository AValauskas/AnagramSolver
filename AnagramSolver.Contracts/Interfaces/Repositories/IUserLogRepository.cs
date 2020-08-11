using AnagramSolver.Contracts.Enums;
using AnagramSolver.EF.DatabaseFirst.Models;
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
        public Task<IEnumerable<UserLog>> GetByIP(string ip);
    }
}
