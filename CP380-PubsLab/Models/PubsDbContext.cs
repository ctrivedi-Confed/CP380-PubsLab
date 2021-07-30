using System;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=localhost\\SQLEXPRESS01;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        // TODO: Add DbSets

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO
            modelBuilder.Entity<Sales>().HasKey(a => new { a.Store_id, a.Title_id });

            modelBuilder
                .Entity<Stores>()
                .HasMany(t => t.Titles)
                .WithMany(s => s.Stores)
                .UsingEntity<Sales>(
                    j => j
                           .HasOne(t => t.Titles)
                           .WithMany(st => st.Sales)
                           .HasForeignKey(t => t.Title_id),
                    j => j
                           .HasOne(t => t.Stores)
                           .WithMany(st => st.Sales)
                           .HasForeignKey(t => t.Store_id)
                );
        }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Jobs> Jobs { get; set; }

        public DbSet<Titles> Titles { get; set; }

        public DbSet<Stores> Stores { get; set; }
    }


    public class Titles
    {
        // TODO
        [Key]
        [Column("title_id")]
        public string Title_id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        public ICollection<Stores> Stores { get; set; }

        public List<Sales> Sales { get; set; }

    }


    public class Stores
    {
        // TODO
        [Key]
        [Column("stor_id")]
        public string Store_id { get; set; }

        [Column("stor_name")]
        public string Store_name { get; set; }

        public ICollection<Titles> Titles { get; set; }

        public List<Sales> Sales { get; set; }
    }

    public class Sales
    {
        // TODO
        [ForeignKey("Stores")]
        [Column("stor_id")]
        public string Store_id { get; set; }

        public Titles Titles { get; set; }

        public Stores Stores { get; set; }

        [ForeignKey("Titles")]
        [Column("title_id")]
        public string Title_id { get; set; }
    }

    public class Employee
    {
        // TODO
        [Key]
        [Column("emp_id")]
        public string Emp_id { get; set; }

        [Column("fname")]
        public string First_name { get; set; }

        [Column("lname")]
        public string Last_name { get; set; }

        [ForeignKey("Jobs")][Column("job_id")]
        public Int16 Id { get; set; }

        public Jobs Jobs { get; set; }

    }

    public class Jobs
    {
        // TODO
        [Key]
        [Column("job_id")]
        public Int16 Job_id { get; set; }

        [Column("job_desc")]
        public string Job_desc { get; set; }

        public List<Employee> Employee { get; set; }

    }
}
