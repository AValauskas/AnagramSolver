using AnagramSolver.Contracts.Enums;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnagramSolver.Contracts.Interfaces.Repositories
{
    public interface IUserLogRepository
    {
        public Task CreateLog(UserLogEntity log);
        public Task<IEnumerable<UserLogEntity>> GetLogs();
        public Task<IEnumerable<UserLogEntity>> GetByIP(string ip);
        public Task<IEnumerable<string>> GetAllIps();
    }
}
