using Microsoft.EntityFrameworkCore;

namespace ConferenceMonitorApi.Models {
    public class ConfDbContext : DbContext {
        public DbSet<Conference> Conferences { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=./conference.db");
        }
    }
}