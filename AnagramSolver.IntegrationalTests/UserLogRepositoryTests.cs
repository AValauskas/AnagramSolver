using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.EntityFramework;
using AnagramSolver.EF.CodeFirst;
using AnagramSolver.EF.CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace AnagramSolver.IntegrationalTests
{
    
    public class UserLogRepositoryTests
    {
        private IUserLogRepository _userLogRepository;   
        protected AnagramSolverDBContext context;
        protected IDbContextTransaction transaction;

        private UserLogEntity _userLogEntity;

        [SetUp]
        public void Setup()
        {
           var options = new DbContextOptionsBuilder<AnagramSolverDBContext>()
                .UseSqlServer(Settings.TestingConnectionString)
                .Options;

            context = new AnagramSolverDBContext(options);
            transaction = context.Database.BeginTransaction();
            _userLogRepository = new UserLogRepositoryEF(context);
            _userLogEntity = new UserLogEntity()
            {
                Time = DateTime.Now,
                Type = TaskType.CreateWord,
                UserIp = "211.211.211",
                Anagrams ="sula; alus",
                SearchedWord="sula"
            };

          
        }

        [TearDown]
        public void Dispose()
        {
            transaction.Rollback();
            transaction.Dispose();
            context.Dispose();
        }

        [Test]
        public async Task CreateLog_SuccesfullyAdded()
        {        
            await _userLogRepository.CreateLog(_userLogEntity);
            await context.SaveChangesAsync();

            var userLogs = await _userLogRepository.GetLogs();

            Assert.IsNotNull(userLogs);
            Assert.AreEqual(1, userLogs.ToList().Count);
        }


        [Test]
        [TestCase ("211.211.2110")]
        public async Task GetByIP_Insert100Values2DifferentIp_Returns50(string ip)
        {
            await InsertLogs();          
            await context.SaveChangesAsync();

            var userLogs = await _userLogRepository.GetByIP(ip);
            var logs = userLogs.ToList();

            Assert.IsNotNull(userLogs);
            Assert.AreEqual(ip, logs[0].UserIp);
            Assert.AreEqual(50, logs.Count);
        }

        [Test]
        public async Task GetLogs_Insert100Values_Returns100()
        {
            await InsertLogs();

            var userLogs = await _userLogRepository.GetLogs();
            var logs = userLogs.ToList();

            Assert.IsNotNull(userLogs);
            Assert.AreEqual(100, logs.Count);
        }

        private async Task InsertLogs()
        {
            for (int a = 0; a < 2; a++)
                for (int i = 0; i < 50; i++)
                {
                    var userLog = new UserLogEntity();
                    userLog.SearchedWord = _userLogEntity.SearchedWord + i.ToString();
                    userLog.Anagrams = _userLogEntity.Anagrams + i.ToString();
                    userLog.SearchedWord = _userLogEntity.SearchedWord + i.ToString();
                    userLog.Time = _userLogEntity.Time;
                    userLog.UserIp = _userLogEntity.UserIp + a.ToString();
                    await _userLogRepository.CreateLog(userLog);
                }
            await context.SaveChangesAsync();
        }

        [Test]
        [TestCase("211.211.2110", "211.211.2111")]
        public async Task GetAllIps_Insert100Values_Returns2Ips(string ip1, string ip2)
        {
            await InsertLogs();
            await context.SaveChangesAsync();

            var userLogs = await _userLogRepository.GetAllIps();
            var ips = userLogs.ToList();

            Assert.IsNotNull(userLogs);
            Assert.AreEqual(ip1, ips[0]);
            Assert.AreEqual(ip2, ips[1]);
            Assert.AreEqual(2, ips.Count);
        }
    }
}