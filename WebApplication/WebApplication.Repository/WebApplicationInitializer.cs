using Microsoft.EntityFrameworkCore;
using System.Linq;
using WebApplication.Repository.Entities;

namespace WebApplication.Repository
{
    public class WebApplicationInitializer
    {
        public static void Initialize(WebApplicationDbContext context)
        {
            // Apply all pending migrations
            context.Database.Migrate();

            if (!context.Users.Any())
            {
                context.HighScores.AddRange(new HighScore { Name = "Jim", Score = 10000 },
                                            new HighScore { Name = "Mary", Score = 3000 },
                                            new HighScore { Name = "Paul", Score = 9500 },
                                            new HighScore { Name = "Kim", Score = 2500 },
                                            new HighScore { Name = "John", Score = 8000 });

                context.SaveChanges();
            }
        }
    }
}
