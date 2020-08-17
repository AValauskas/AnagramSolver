using AnagramSolver.EF.CodeFirst.Models;
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
        
        public DbSet<WordEntity> Word { get; set; }
        public DbSet<CachedWordEntity> CachedWord { get; set; }
        public DbSet<UserLogEntity> UserLog { get; set; }
        public DbSet<CachedWordWord> CachedWordWord { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnagramSolverDBTesting; Integrated Security = True;");
            // optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=AnagramSolverCFDB; Integrated Security = True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWordWord>().HasKey(sc => new { sc.WordId, sc.CachedWordId });
        }

    }
}
