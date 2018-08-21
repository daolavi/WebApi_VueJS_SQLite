using AutoMapper;
using Microsoft.Extensions.Configuration;
using WebApplication.Repository;
using WebApplication.Services;
using Xunit;

namespace WebApplication.Tests
{
    public class BaseTest : IClassFixture<TestContext>
    {
        private static bool isInitialized = false;

        protected WebApplicationDbContext DbContext;

        protected IConfiguration Config;

        protected IMapper MapperInstance;

        public BaseTest(TestContext testContext)
        {
            DbContext = testContext.DbContext;
            Config = testContext.Config;
            if (!isInitialized)
            {
                Mapper.Initialize(config => config.AddProfile(new MapperProfile()));
                isInitialized = true;
            }
            MapperInstance = Mapper.Instance;
        }
    }
}
