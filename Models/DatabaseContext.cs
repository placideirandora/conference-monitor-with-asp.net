using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi.Models {
    public class DatabaseContext : DbContext {
        // Create database
        public DbSet<Conference> Conferences { get; set; }

        // Override database configuration 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=./Conference.db");
        }
    }
}