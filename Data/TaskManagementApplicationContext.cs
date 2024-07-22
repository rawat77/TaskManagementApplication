using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManagementApplication.Models;

namespace TaskManagementApplication.Data
{
    public class TaskManagementApplicationContext : DbContext
    {
        public TaskManagementApplicationContext (DbContextOptions<TaskManagementApplicationContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)            // User has one Team
                .WithMany(t => t.Members)         // Team can have many Users
                .HasForeignKey(u => u.TeamId); // Define foreign key

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<TaskManagementApplication.Models.Tasks> Tasks { get; set; } = default!;
        public DbSet<TaskManagementApplication.Models.User> User { get; set; } = default!;
        public DbSet<TaskManagementApplication.Models.Team> Team { get; set; } = default!;
        public DbSet<TaskManagementApplication.Models.Note> Note { get; set; } = default!;
        public DbSet<TaskManagementApplication.Models.Document> Document { get; set; } = default!;

    }
}
