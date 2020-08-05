﻿// <auto-generated />
using System;
using AnagramSolver.EF.DatabaseFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AnagramSolver.EF.DatabaseFirst.Migrations
{
    [DbContext(typeof(AnagramSolverDBContext))]
    partial class AnagramSolverDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AnagramSolver.Contracts.Models.CachedWord", b =>
                {
                    b.Property<int>("CachedWordId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CachedWordId");

                    b.ToTable("CachedWords");
                });

            modelBuilder.Entity("AnagramSolver.Contracts.Models.CachedWord_WordEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("CachedWordId")
                        .HasColumnType("int");

                    b.Property<int?>("WordId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CachedWordId");

                    b.HasIndex("WordId");

                    b.ToTable("CachedWord_WordEntityes");
                });

            modelBuilder.Entity("AnagramSolver.Contracts.Models.UserLog", b =>
                {
                    b.Property<string>("UserLogId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Anagrams")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserIp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserLogId");

                    b.ToTable("UserLogs");
                });

            modelBuilder.Entity("AnagramSolver.Contracts.Models.WordModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("LanguagePart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SortedWord")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Word")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Words");
                });

            modelBuilder.Entity("AnagramSolver.Contracts.Models.CachedWord_WordEntity", b =>
                {
                    b.HasOne("AnagramSolver.Contracts.Models.CachedWord", "CachedWord")
                        .WithMany()
                        .HasForeignKey("CachedWordId");

                    b.HasOne("AnagramSolver.Contracts.Models.WordModel", "Word")
                        .WithMany()
                        .HasForeignKey("WordId");
                });
#pragma warning restore 612, 618
        }
    }
}
