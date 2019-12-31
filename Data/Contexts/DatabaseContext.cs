using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi.Models {
    public class DatabaseContext : DbContext {
        // Create tables
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options){}

        // Override database configuration 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if(!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite("Filename=./ConferenceMonitor.db");
            }
        }
    }
}