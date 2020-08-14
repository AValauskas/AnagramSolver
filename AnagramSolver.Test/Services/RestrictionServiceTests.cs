using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Enums;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AnagramSolver.Test.Services
{
    public class RestrictionServiceTests
    {
        private IUserLogRepository _userLogRepository;
        private RestrictionService _restrictionService;
        private List<UserLog> _logs;
        private UserLog _log;

        [SetUp]
        public void Setup()
        {
            _userLogRepository = Substitute.For<IUserLogRepository>();
            _restrictionService = new RestrictionService(_userLogRepository);
            _log = new UserLog()
            {
                Anagrams = "alus;sula",
                SearchedWord = "ulsa",
                Time = DateTime.Now,
                Type = TaskType.SearchAnagram,
                UserIp = "222.222",
                UserLogId = 3
            };
            _logs = new List<UserLog>() { _log };
        }

        [Test]
        public async Task CheckIfActionCanBePerformed_ReturnsTrue()
        {
            _userLogRepository.GetByIP(Arg.Any<string>()).Returns(_logs);

            var result = await _restrictionService.CheckIfActionCanBePerformed();
            Assert.IsTrue(result);
        }
        [Test]
        public async Task CheckIfActionCanBePerformed_ReturnsFalse()
        {
            for (int i = 0; i < 500; i++)
                _logs.Add(_log);
            _userLogRepository.GetByIP(Arg.Any<string>()).Returns(_logs);

            var result = await _restrictionService.CheckIfActionCanBePerformed();
            Assert.IsFalse(result);
        }

    }
}
