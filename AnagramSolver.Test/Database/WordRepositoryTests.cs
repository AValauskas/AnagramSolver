
using NUnit.Framework;
using AnagramSolver.Contracts.Interfaces;
using AnagramSolver.Data;
using AnagramSolver.Contracts.Interfaces.Repositories;
using AnagramSolver.Data.Database;
using AnagramSolver.Data.EntityFramework;
using AnagramSolver.EF.CodeFirst;
using System.Threading.Tasks;

namespace WordModelSolver.Test.Databse
{
    public class WordRepositoryTests
    {
        private TestRepo test;
        [SetUp]
        public void Setup()
        {
            test = new TestRepo(new AnagramSolverDBContext());
       
        }

        [Test]
        public async Task Testitng()
        {
            await test.Test();
        }

    }
}