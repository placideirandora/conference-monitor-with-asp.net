using Microsoft.EntityFrameworkCore;

namespace api_with_asp.net.Models {
    public class AppDbContext : DbContext {
        public DbSet<Conference> Conferences { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseSqlite("Filename=./conference.db");
        }
    }
}