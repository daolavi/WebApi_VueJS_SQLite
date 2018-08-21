using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WebApplication.Constants;
using WebApplication.Repository;
using WebApplication.Repository.Entities;

namespace WebApplication.Services.AuthenticationService
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration config;

        private WebApplicationDbContext dbContext;

        private IMapper mapper;

        private IPasswordHasher<User> passwordHasher;

        public AuthenticationService(IConfiguration config, WebApplicationDbContext dbContext, IMapper mapper, IPasswordHasher<User> passwordHasher)
        {
            this.config = config;
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.passwordHasher = passwordHasher;
        }

        public Models.User Authenticate(string username, string password)
        {
            var accountEntity = dbContext.Users.FirstOrDefault(x => x.Username == username);
            Models.User accountModel = null;
            if (accountEntity != null)
            {
                var verifyResult = passwordHasher.VerifyHashedPassword(accountEntity, accountEntity.Password, password);
                if (verifyResult == PasswordVerificationResult.Success)
                {
                    accountModel = mapper.Map<Models.User>(accountEntity);
                }
            }
            return accountModel;
        }

        public string BuildToken(Models.User account)
        {
            var claims = new[]
            {
                new Claim(WebApplicationClaimNames.AccountId,account.Id.ToString()),
                new Claim(WebApplicationClaimNames.Username,account.Username),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(config["Jwt:Issuer"],
                                            config["Jwt:Issuer"],
                                            claims,
                                            expires: DateTime.Now.AddMinutes(30),
                                            signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
