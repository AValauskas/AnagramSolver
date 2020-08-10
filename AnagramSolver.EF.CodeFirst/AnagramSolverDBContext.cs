using AnagramSolver.BusinessLogic.Utils;
using AnagramSolver.EF.DatabaseFirst.Models;
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
        public DbSet<Word> Word { get; set; }
        public DbSet<CachedWord> CachedWord { get; set; }
        public DbSet<UserLog> UserLog { get; set; }
        public DbSet<CachedWordWord> CachedWordWord { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Settings.ConnectionString);
        }
    }
}
