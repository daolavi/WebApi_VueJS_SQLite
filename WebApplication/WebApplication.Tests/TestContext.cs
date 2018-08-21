using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication.Repository;

namespace WebApplication.Tests
{
    public class TestContext
    {
        public WebApplicationDbContext DbContext;

        public IConfiguration Config;

        public TestContext()
        {
            var options = new DbContextOptionsBuilder<WebApplicationDbContext>().UseInMemoryDatabase(databaseName: "TestDb").Options;
            DbContext = new WebApplicationDbContext(options);
            DbContext.Users.RemoveRange(DbContext.Users);
            DbContext.HighScores.RemoveRange(DbContext.HighScores);
            DbContext.SaveChanges();

            Config = A.Fake<IConfiguration>();
        }
    }
}
