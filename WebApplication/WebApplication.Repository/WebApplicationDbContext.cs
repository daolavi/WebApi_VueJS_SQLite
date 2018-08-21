using Microsoft.EntityFrameworkCore;
using WebApplication.Repository.Entities;

namespace WebApplication.Repository
{
    public class WebApplicationDbContext : DbContext
    {
        public WebApplicationDbContext(DbContextOptions<WebApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<HighScore> HighScores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<HighScore>().ToTable("HighScore");
        }
    }
}
