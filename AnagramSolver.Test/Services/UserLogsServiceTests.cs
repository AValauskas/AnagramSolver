using AnagramSolver.BusinessLogic.Services;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AutoMapper;
using NSubstitute;
using NUnit.Framework;
using AnagramSolver.WebApp.Profiles;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using AnagramSolver.EF.CodeFirst.Models;
using System;
using AnagramSolver.Contracts.Enums;

namespace AnagramSolver.Test.Services
{
    [TestFixture]
    public class UserLogsServiceTests
    {
        private IUserLogRepository _userLogRepository;
        private LogService _userLogService;
        private List<UserLogEntity> _logs;
        private UserLogEntity _log;
        private List<string> _anagrams;

        [SetUp]
        public void Setup()
        {
            _userLogRepository = Substitute.For<IUserLogRepository>();
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            _userLogService = new LogService(_userLogRepository, mapper);
            _log = new UserLogEntity()
            {
                Anagrams = "alus;sula",
                SearchedWord = "ulsa",
                Time = DateTime.Now,
                Type = TaskType.SearchAnagram,
                UserIp = "222.222",
                Id = 3
            };
            _logs = new List<UserLogEntity>() {_log};

            _anagrams = new List<string>() { "alus", "sula" };
        }

        [Test]
        public async Task GetAllIpsOfLogs_ShouldGetAllWorkersIp()
        {
            _userLogRepository.GetAllIps().Returns(new List<string>
            { "555.555",
              "210.000"  
            });

            var enumerableResult = await _userLogService.GetAllIpsOfLogs();
            var result = enumerableResult.ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual("555.555", result[0]);
            Assert.AreEqual("210.000", result[1]);

            await _userLogRepository.Received().GetAllIps();
        }

        [Test]
        public async Task GetAllLogs_ShouldGetAllLogs()
        {
            _userLogRepository.GetLogs().Returns(_logs);

            var enumerableResult = await _userLogService.GetAllLogs();
            var result = enumerableResult.ToList();

            Assert.IsNotNull(result);
            Assert.IsNotInstanceOf<Contracts.Models.UserLog>(result);

            await _userLogRepository.Received().GetLogs();
        }

        [Test]
        public async Task CreateLog()
        {
            var counter = 0;
            _userLogRepository.When(x => x.CreateLog(Arg.Any<UserLogEntity>()))
                .Do(x => counter++);

            await _userLogService.CreateLog("ulsa", _anagrams, TaskType.SearchAnagram);           

            Assert.AreEqual(1, counter);    
            await _userLogRepository.Received().CreateLog(Arg.Any<UserLogEntity>());
        }

    }
}
