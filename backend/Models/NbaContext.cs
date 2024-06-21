using Microsoft.EntityFrameworkCore;

namespace backend.Models
{
    public class NbaContext : DbContext
    {
        public NbaContext(DbContextOptions<NbaContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
    }
}