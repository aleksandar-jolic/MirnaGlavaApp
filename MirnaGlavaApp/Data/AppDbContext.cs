using Microsoft.EntityFrameworkCore;
using MirnaGlavaApp.Models;

namespace MirnaGlavaApp.Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _dbPath;

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<TaskList> Lists { get; set; }

        public AppDbContext(string dbPath)
        {
            _dbPath = dbPath;
            Database.EnsureCreated(); // pravi bazu ako ne postoji
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_dbPath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Odnosi 1:N između TaskList i TaskItem
            modelBuilder.Entity<TaskList>()
                .HasMany(l => l.Tasks)
                .WithOne(t => t.TaskList)
                .HasForeignKey(t => t.TaskListId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
