using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using WebApplication.Repository.Entities;
using WebApplication.Services.AuthenticationService;
using Xunit;

namespace WebApplication.Tests
{
    public class AuthenticationServiceTest : BaseTest
    { 
        private IPasswordHasher<User> passwordHasher;

        public AuthenticationServiceTest(TestContext testContext) : base(testContext)
        {
            passwordHasher = new PasswordHasher<User>();
        }

        [Fact]
        public void Authenticate_Success()
        {
            var user = new User { Username = "user1" };
            var hashedPassword = passwordHasher.HashPassword(user, "test");
            DbContext.Set<User>().Add(new User { Username = "user1", Password = hashedPassword });
            DbContext.SaveChanges();

            var authenticateService = new AuthenticationService(Config, DbContext, MapperInstance, passwordHasher);
            var model = authenticateService.Authenticate("user1", "test");
            Assert.NotNull(model);
        }

        [Fact]
        public void Authenticate_Fail()
        {
            var user = new User { Username = "user2" };
            var hashedPassword = passwordHasher.HashPassword(user, "test");
            DbContext.Set<User>().Add(new User { Username = "user2", Password = hashedPassword });
            DbContext.SaveChanges();

            var authenticateService = new AuthenticationService(Config, DbContext, MapperInstance, passwordHasher);
            var model = authenticateService.Authenticate("user2", "testfail");
            Assert.True(model == null);
        }
    }
}
