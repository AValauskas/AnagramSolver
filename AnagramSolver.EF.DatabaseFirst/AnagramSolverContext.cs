using System;
using AnagramSolver.EF.DatabaseFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AnagramSolver.EF.DatabaseFirst
{
    public partial class AnagramSolverContext : DbContext
    {
        public AnagramSolverContext()
        {
        }

        public AnagramSolverContext(DbContextOptions<AnagramSolverContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CachedWord> CachedWord { get; set; }
        public virtual DbSet<CachedWordWord> CachedWordWord { get; set; }
        public virtual DbSet<UserLog> UserLog { get; set; }
        public virtual DbSet<Word> Word { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AnagramSolver;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CachedWord>(entity =>
            {
                entity.Property(e => e.Word)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();
            });

            modelBuilder.Entity<CachedWordWord>(entity =>
            {
                entity.HasKey(e => new { e.WordId, e.CachedWordId });

                entity.ToTable("CachedWord_Word");

                entity.HasOne(d => d.CachedWord)
                    .WithMany(p => p.CachedWordWord)
                    .HasForeignKey(d => d.CachedWordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CachedWord_Word_CachedWord");

                entity.HasOne(d => d.Word)
                    .WithMany(p => p.CachedWordWord)
                    .HasForeignKey(d => d.WordId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CachedWord_Word_Word");
            });

            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.Property(e => e.Anagrams)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.SearchedWord)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.Time).HasColumnType("datetime");

                entity.Property(e => e.UserIp)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.SortedWord)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsFixedLength();

                entity.Property(e => e.Word1)
                    .IsRequired()
                    .HasColumnName("Word")
                    .HasMaxLength(255)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
