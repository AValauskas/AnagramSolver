using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace AnagramSolver.EF.CodeFirst
{
    public class AnagramSolverDBContext : DbContext
    {
        public AnagramSolverDBContext()
        {
        }
        public AnagramSolverDBContext(DbContextOptions<AnagramSolverDBContext> options)
            : base(options)
        {
        }
        public DbSet<WordModel> Words { get; set; }
        public DbSet<CachedWord> CachedWords { get; set; }
        public DbSet<UserLog> UserLogs { get; set; }
        public DbSet<CachedWord_WordEntity> CachedWord_WordEntityes { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }
    }
}
