using DataLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Database
{
    public class DatabaseContext : DbContext
    {
        public DbSet<DatabaseUser> Users { get; set; }
        public DbSet<LogEntry> Logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Lachezar\\source\\repos\\PS_39_Lacho\\DataLayer\\bin\\Debug\\net8.0\\users.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DatabaseUser>()
        .Property(u => u.Id)
        .ValueGeneratedOnAdd();

            // Seed data with ALL required properties
            modelBuilder.Entity<DatabaseUser>().HasData(
                new DatabaseUser
                {
                    Id = -1,
                    Names = "Admin",
                    Password = "admin123",
                    Email = "admin@example.com", // Provide value for Email
                    Role = Welcome.Others.UserRolesEnum.ADMIN,
                    FacultyNumber = "000000",
                    Expires = DateTime.Now.AddYears(1)
                }
            );

            // Remove not null constraint from FacultyNumber
            modelBuilder.Entity<DatabaseUser>()
                .Property(u => u.FacultyNumber)
                .IsRequired(false);
        }
    }
}
